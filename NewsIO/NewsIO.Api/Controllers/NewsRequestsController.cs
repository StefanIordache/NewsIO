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

        // GET - /api/NewsRequest/{id}
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
        public async Task<IActionResult> AddNewNewsRequest([FromBody] NewsRequestViewModel newsRequestVM)
        {
            try
            {
                var tokenString = Request.Headers["Authorization"].ToString();
                var userName = JwtHelper.GetUserNameFromJwt(tokenString);
                var userId = JwtHelper.GetUserIdFromJwt(tokenString);

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

                        int newsRequestId = await NewsRequestService.AddAsync(newsRequest);

                        if (!string.IsNullOrEmpty(tokenString))
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
    }
}