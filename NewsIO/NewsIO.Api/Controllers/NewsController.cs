using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.Utils;
using NewsIO.Data.Models.Application;
using NewsIO.Services.Intefaces;

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
    }
}