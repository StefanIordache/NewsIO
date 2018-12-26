using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.Utils;
using NewsIO.Api.ViewModels;
using NewsIO.Data.Models.Application;
using NewsIO.Services.Intefaces;
using static NewsIO.Api.Utils.Models;

namespace NewsIO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsRequestsController : ControllerBase
    {
        public INewsRequestService NewsRequestService { get; set; }

        public ICategoryService CategoryService { get; set; }

        private readonly IMapper Mapper;

        public NewsRequestsController(INewsRequestService newsRequestService, ICategoryService categoryService, IMapper mapper)
        {
            NewsRequestService = newsRequestService;
            CategoryService = categoryService;
            Mapper = mapper;
        }

        // GET - /api/NewsRequests/{pageSize?}/{pageNo?}
        [HttpGet]
        public async Task<IActionResult> GetNewsRequestAsync(int pageSize = 0, int pageNo = 0)
        {
            try
            {
                IEnumerable<NewsRequest> newsRequests;
                PagedResult<NewsRequest> pagedResult = new PagedResult<NewsRequest>();
                int numberOfRecords = 0;

                if (pageSize > 0)
                {
                    (newsRequests, numberOfRecords) = await NewsRequestService.GetWithPaginationAsync<NewsRequest>(pageSize, pageNo);
                }
                else
                {
                    newsRequests = await NewsRequestService.GetAllAsync<NewsRequest>();
                }

                if (newsRequests == null || !newsRequests.Any())
                {
                    return NotFound();
                }

                return Ok(newsRequests);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET - /api/NewsRequests/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsRequestById(int id)
        {
            try
            {
                NewsRequest newsRequest = await NewsRequestService.GetByIdAsync<NewsRequest>(id);

                if (newsRequest != null)
                {
                    return Ok(newsRequest);
                }

                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST - /api/NewsRequests/add
        [Authorize(Roles = "Administrator, Moderator, Member")]
        [HttpPost("add")]
        public async Task<IActionResult> AddNewsRequest([FromBody] NewsRequestViewModel newsRequestVM)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userName = JwtHelper.GetUserNameFromJwt(token);
                var userId = JwtHelper.GetUserIdFromJwt(token);

                try
                {
                    var category = await CategoryService.GetByIdAsync<Category>(newsRequestVM.CategoryId);

                    if (category != null)
                    {
                        var newsRequest = Mapper.Map<NewsRequest>(newsRequestVM);

                        newsRequest.RequestDate = DateTime.Now;
                        newsRequest.RequestedBy = userName;
                        newsRequest.RequestedById = userId;
                        newsRequest.Category = category;
                        newsRequest.Status = "New";
                        newsRequest.IsClosed = false;

                        int newsRequestId = await NewsRequestService.AddAsync(newsRequest);

                        if (!string.IsNullOrEmpty(token))
                        {
                            await NewsRequestService.PublishEntity<NewsRequest>(newsRequestId, userId, userName);
                        }

                        return Ok(new Response
                        {
                            Status = ResponseType.Successful,
                            Value = newsRequest
                        });
                    }

                    return Ok(new Response { Status = ResponseType.Failed });
                }
                catch (Exception e)
                {
                    return Ok(new Response { Status = ResponseType.Failed, Message = e.Message });
                }
            }
            catch
            {
                return Ok(new Response { Status = ResponseType.Failed });
            }
        }

        // POST - /api/NewsRequests/close/{id}
        [Authorize(Roles = "Administrator, Moderator, Member")]
        [HttpPost("close/{id}")]
        public async Task<IActionResult> CloseNewsRequest(int id)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                var newsRequest = await NewsRequestService.GetByIdAsync<NewsRequest>(id);

                if (newsRequest == null)
                {
                    return NotFound();
                }

                if (JwtHelper.CheckIfUserIsMember(token)  && newsRequest.RequestedById != JwtHelper.GetUserIdFromJwt(token))
                {
                    return Forbid();
                }

                bool updateResult = false;

                if (!string.IsNullOrEmpty(token))
                {
                    updateResult = await NewsRequestService.CloseNewsRequestAsync(newsRequest);
                    await NewsRequestService.PublishEntity<NewsRequest>(newsRequest.Id, JwtHelper.GetUserIdFromJwt(token), JwtHelper.GetUserNameFromJwt(token));
                }

                if (updateResult)
                {
                    return Ok(new Response { Status = ResponseType.Successful });
                }

                return Ok(new Response { Status = ResponseType.Failed });
            }
            catch
            {
                return Ok(new Response { Status = ResponseType.Failed });
            }
        }

        // POST - /api/NewsRequests/open/{id}
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost("open/{id}")]
        public async Task<IActionResult> OpenNewsRequest(int id)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                var newsRequest = await NewsRequestService.GetByIdAsync<NewsRequest>(id);

                if (newsRequest == null)
                {
                    return NotFound();
                }

                bool updateResult = false;

                if (!string.IsNullOrEmpty(token))
                {
                    updateResult = await NewsRequestService.OpenNewsRequestAsync(newsRequest);
                    await NewsRequestService.PublishEntity<NewsRequest>(newsRequest.Id, JwtHelper.GetUserIdFromJwt(token), JwtHelper.GetUserNameFromJwt(token));
                }

                if (updateResult)
                {
                    return Ok(new Response { Status = ResponseType.Successful });
                }

                return Ok(new Response { Status = ResponseType.Failed });
            }
            catch
            {
                return Ok(new Response { Status = ResponseType.Failed });
            }
        }

        // POST - /api/NewsRequests/editInfo/{id}
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditNewsRequest(int id, [FromBody] NewsRequest newsRequest)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                await NewsRequestService.UpdateAsync<NewsRequest>(id, newsRequest);

                if (!string.IsNullOrEmpty(token))
                {
                    await NewsRequestService.UpdateLastEdit<NewsRequest>(id, JwtHelper.GetUserIdFromJwt(token), JwtHelper.GetUserNameFromJwt(token));
                }

                return Ok(new Response
                {
                    Status = ResponseType.Successful,
                    Value = newsRequest
                });
            }
            catch
            {
                return Ok(new Response { Status = ResponseType.Failed });
            }
        }

        // POST - api/NewsRequests/Delete/{id}
        [Authorize(Roles = "Administrator")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNewsRequest(int id)
        {
            try
            {
                await NewsRequestService.Delete<NewsRequest>(id);

                return Ok(new Response
                {
                    Status = ResponseType.Successful
                });
            }
            catch
            {
                return Ok(new Response { Status = ResponseType.Failed });
            }
        }

        // POST - api/NewsRequests/changeCategory/{newsRequestId}/{categoryId}
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost("changeCategory")]
        public async Task<IActionResult> ChangeNewsRequestCategory(int newsRequestId, int categoryId)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                var newsRequest = await NewsRequestService.GetByIdAsync<NewsRequest>(newsRequestId);

                var category = await CategoryService.GetByIdAsync<Category>(categoryId);

                if (category == null)
                {
                    return Ok(new Response { Status = ResponseType.Failed, Message = "Category not found" });
                }
                if (newsRequest == null)
                {
                    return Ok(new Response { Status = ResponseType.Failed, Message = "News Request not found" });
                }

                var result = await NewsRequestService.ChangeNewsRequestCategoryAsync(newsRequest, category);

                if (!result)
                {
                    return Ok(new Response { Status = ResponseType.Failed });
                }

                return Ok(new Response
                {
                    Status = ResponseType.Successful
                });
            }
            catch
            {
                return Ok(new Response { Status = ResponseType.Failed });
            }
        }
    }
}