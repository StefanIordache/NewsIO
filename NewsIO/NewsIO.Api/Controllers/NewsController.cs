using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.Utils;
using NewsIO.Api.Utils.ImageServices.Implementations;
using NewsIO.Api.Utils.ImageServices.Interfaces;
using NewsIO.Api.ViewModels;
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

        private ICategoryService CategoryService { get; set; }

        private readonly IImageHandler ImageHandler;

        private IImageService ImageService { get; set; }

        private readonly IMapper Mapper;

        public NewsController(INewsService newsService, ICategoryService categoryService, IImageHandler imageHandler, IImageService imageService, IMapper mapper)
        {
            NewsService = newsService;
            CategoryService = categoryService;
            ImageHandler = imageHandler;
            ImageService = imageService;
            Mapper = mapper;
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

                if (news != null)
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
        public async Task<IActionResult> AddNews(NewsViewModel newsVM)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userName = JwtHelper.GetUserNameFromJwt(token);
                var userId = JwtHelper.GetUserIdFromJwt(token);

                News entry = Mapper.Map<News>(newsVM);

                try
                {
                    var category = await CategoryService.GetByIdAsync<Category>(entry.CategoryId);

                    if (category != null)
                    {
                        entry.Category = category;

                        var thumbnailUrl = await ImageHandler.UploadImage(newsVM.Thumbnail);

                        if (string.IsNullOrEmpty(thumbnailUrl))
                        {
                            return Ok(new Response
                            {
                                Status = ResponseType.Failed,
                                Message = "Failed thumbnail upload"
                            });
                        }

                        entry.ThumbnailUrl = thumbnailUrl;

                        int entryId = await NewsService.AddAsync(entry);

                        if (entryId > 0)
                        {
                            var images = Request.Form.Files.ToList().Skip(1);

                            foreach (var image in images)
                            {
                                var imageUrl = await ImageHandler.UploadImage(image);

                                if (!string.IsNullOrEmpty(imageUrl))
                                {
                                    var imageId = await ImageService.AddAsync(new Image { Url = imageUrl, NewsId = entryId });

                                    if (imageId > 0 && !string.IsNullOrEmpty(token))
                                    {
                                        await ImageService.PublishEntity<Image>(imageId, userId, userName);
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(token))
                            {
                                await NewsService.PublishEntity<News>(entryId, userId, userName);
                            }
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

        // POST - /api/News/addExternal
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost("addExternal")]
        public async Task<IActionResult> AddExternalNews(NewsViewModel newsVM)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userName = JwtHelper.GetUserNameFromJwt(token);
                var userId = JwtHelper.GetUserIdFromJwt(token);

                News entry = Mapper.Map<News>(newsVM);

                try
                {
                    var category = await CategoryService.GetByIdAsync<Category>(entry.CategoryId);

                    if (category != null)
                    {
                        entry.Category = category;

                        var thumbnailUrl = await ImageHandler.UploadImage(newsVM.Thumbnail);

                        if (string.IsNullOrEmpty(thumbnailUrl))
                        {
                            return Ok(new Response
                            {
                                Status = ResponseType.Failed,
                                Message = "Failed thumbnail upload"
                            });
                        }

                        entry.ThumbnailUrl = thumbnailUrl;

                        int entryId = await NewsService.AddAsync(entry);

                        if (!string.IsNullOrEmpty(token) && entryId > 0)
                        {
                            await NewsService.PublishEntity<News>(entryId, userId, userName);
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

        // POST - api/News/changeCategory/{newsId}/{categoryId}
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost("changeCategory")]
        public async Task<IActionResult> ChangeNewsCategory(int newsId, int categoryId)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                var news = await NewsService.GetByIdAsync<News>(newsId);

                var category = await CategoryService.GetByIdAsync<Category>(categoryId);

                if (category == null)
                {
                    return Ok(new Response { Status = ResponseType.Failed, Message = "Category not found" });
                }
                if (news == null)
                {
                    return Ok(new Response { Status = ResponseType.Failed, Message = "News Request not found" });
                }
                if (JwtHelper.CheckIfUserIsModerator(token) && news.PublishedById != JwtHelper.GetUserIdFromJwt(token))
                {
                    return Forbid();
                }

                var result = await NewsService.ChangeNewsCategoryAsync(news, category);

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