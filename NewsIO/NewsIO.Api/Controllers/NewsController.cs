using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.Utils;
using NewsIO.Data.Models.Application;
using NewsIO.Services.Intefaces;
using static NewsIO.Api.Utils.Models;

namespace NewsIO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private INewsService NewsService { get; set; }

        private INewsRequestService NewsRequestService { get; set; }

        private ICategoryService CategoryService { get; set; }

        public NewsController(INewsService newsService, INewsRequestService newsRequestService, ICategoryService categoryService)
        {
            NewsService = newsService;
            NewsRequestService = newsRequestService;
            CategoryService = categoryService;
        }

        // GET - /api/News/{pageSize?}/{pageNo?}
        [HttpGet]
        public async Task<IActionResult> GetNewsAsync(int pageSize = 0, int pageNo = 0)
        {
            try
            {
                IEnumerable<News> news;
                PagedResult<News> pagedResult = new PagedResult<News>();
                int numberOfRecords = 0;

                if (pageSize > 0)
                {
                    (news, numberOfRecords) = await NewsService.GetWithPaginationAsync<News>(pageSize, pageNo);
                }
                else
                {
                    news = await NewsService.GetAllAsync<News>();
                }

                if (news == null || !news.Any())
                {
                    return NotFound();
                }

                return Ok(news);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET - /api/News/getLatest/{pageSize?}/{pageNo?}
        [HttpGet("getLatest")]
        public async Task<IActionResult> GetLatestNewsAsync(int pageSize = 0, int pageNo = 0)
        {
            try
            {
                IEnumerable<News> news;
                PagedResult<News> pagedResult = new PagedResult<News>();
                int numberOfRecords = 0;

                if (pageSize > 0)
                {
                    (news, numberOfRecords) = await NewsService.GetLatestWithPaginationAsync(pageSize, pageNo);
                }
                else
                {
                    news = await NewsService.GetLatestAsync();
                }

                if (news == null || !news.Any())
                {
                    return NotFound();
                }

                return Ok(news);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET - /api/News/getLatestByCategory/{categoryId}/{pageSize?}/{pageNo?}
        [HttpGet("getLatestByCategory")]
        public async Task<IActionResult> GetLatestNewsByCategoryAsync(int categoryId, int pageSize = 0, int pageNo = 0)
        {
            try
            {
                IEnumerable<News> news;
                PagedResult<News> pagedResult = new PagedResult<News>();
                int numberOfRecords = 0;

                var category = await CategoryService.GetByIdAsync<Category>(categoryId);

                if (category == null)
                {
                    return NotFound();
                }

                if (pageSize > 0)
                {
                    (news, numberOfRecords) = await NewsService.GetLatestByCategoryWithPaginationAsync(category, pageSize, pageNo);
                }
                else
                {
                    news = await NewsService.GetLatestByCategoryAsync(category);
                }

                if (news == null || !news.Any())
                {
                    return NotFound();
                }

                return Ok(news);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET - /api/News/getOldestByCategory/{categoryId}/{pageSize?}/{pageNo?}
        [HttpGet("getOldestByCategory")]
        public async Task<IActionResult> GetOldestNewsByCategoryAsync(int categoryId, int pageSize = 0, int pageNo = 0)
        {
            try
            {
                IEnumerable<News> news;
                PagedResult<News> pagedResult = new PagedResult<News>();
                int numberOfRecords = 0;

                var category = await CategoryService.GetByIdAsync<Category>(categoryId);

                if (category == null)
                {
                    return NotFound();
                }

                if (pageSize > 0)
                {
                    (news, numberOfRecords) = await NewsService.GetOldestByCategoryWithPaginationAsync(category, pageSize, pageNo);
                }
                else
                {
                    news = await NewsService.GetOldestByCategoryAsync(category);
                }

                if (news == null || !news.Any())
                {
                    return NotFound();
                }

                return Ok(news);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET - /api/News/getAlphabeticalByCategory/{categoryId}/{pageSize?}/{pageNo?}
        [HttpGet("getAlphabeticalByCategory")]
        public async Task<IActionResult> GetAlpabeticalNewsByCategoryAsync(int categoryId, int pageSize = 0, int pageNo = 0)
        {
            try
            {
                IEnumerable<News> news;
                PagedResult<News> pagedResult = new PagedResult<News>();
                int numberOfRecords = 0;

                var category = await CategoryService.GetByIdAsync<Category>(categoryId);

                if (category == null)
                {
                    return NotFound();
                }

                if (pageSize > 0)
                {
                    (news, numberOfRecords) = await NewsService.GetAlphabeticalByCategoryWithPaginationAsync(category, pageSize, pageNo);
                }
                else
                {
                    news = await NewsService.GetAlphabeticalByCategoryAsync(category);
                }

                if (news == null || !news.Any())
                {
                    return NotFound();
                }

                return Ok(news);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET - /api/News/getNonAlphabeticalByCategory/{categoryId}/{pageSize?}/{pageNo?}
        [HttpGet("getNonAlphabeticalByCategory")]
        public async Task<IActionResult> GetNonAlpabeticalNewsByCategoryAsync(int categoryId, int pageSize = 0, int pageNo = 0)
        {
            try
            {
                IEnumerable<News> news;
                PagedResult<News> pagedResult = new PagedResult<News>();
                int numberOfRecords = 0;

                var category = await CategoryService.GetByIdAsync<Category>(categoryId);

                if (category == null)
                {
                    return NotFound();
                }

                if (pageSize > 0)
                {
                    (news, numberOfRecords) = await NewsService.GetNonAlphabeticalByCategoryWithPaginationAsync(category, pageSize, pageNo);
                }
                else
                {
                    news = await NewsService.GetNonAlphabeticalByCategoryAsync(category);
                }

                if (news == null || !news.Any())
                {
                    return NotFound();
                }

                return Ok(news);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET - /api/News/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsById(int id)
        {
            try
            {
                News news = await NewsService.GetByIdAsync<News>(id);

                if (news  != null)
                {
                    return Ok(news);
                }

                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST - /api/News/add
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost("add")]
        public async Task<IActionResult> AddNews([FromBody] News entry)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userName = JwtHelper.GetUserNameFromJwt(token);
                var userId = JwtHelper.GetUserIdFromJwt(token);

                try
                {
                    var category = await CategoryService.GetByIdAsync<Category>(entry.CategoryId);

                    if (category != null)
                    { 
                        int entryId = await NewsService.AddAsync(entry);

                        if (!string.IsNullOrEmpty(token))
                        {
                            await NewsService.PublishEntity<NewsRequest>(entryId, userId, userName);
                        }

                        return Ok(new Response
                        {
                            Status = ResponseType.Successful,
                            Value = entry
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

        // POST - /api/News/edit/{id}
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditNews(int id, [FromBody] News news)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                var updatedEntry = await NewsService.GetByIdAsync<News>(id);

                if (updatedEntry == null)
                {
                    return NotFound();
                }

                if (JwtHelper.CheckIfUserIsModerator(token) && news.PublishedById != JwtHelper.GetUserIdFromJwt(token))
                {
                    return Forbid();
                }

                await NewsService.UpdateAsync(id, news);

                if (!string.IsNullOrEmpty(token))
                {
                    await NewsService.UpdateLastEdit<News>(id, JwtHelper.GetUserIdFromJwt(token), JwtHelper.GetUserNameFromJwt(token));
                }

                return Ok(new Response
                {
                    Status = ResponseType.Successful,
                    Value = news
                });
            }
            catch
            {
                return Ok(new Response { Status = ResponseType.Failed });
            }
        }

        // POST - api/News/Delete/{id}
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                var deletedEntry = await NewsService.GetByIdAsync<News>(id);

                if (deletedEntry == null)
                {
                    return NotFound();
                }

                if (JwtHelper.CheckIfUserIsModerator(token) && deletedEntry.PublishedById != JwtHelper.GetUserIdFromJwt(token))
                {
                    return Forbid();
                }

                await NewsService.Delete<News>(id);

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