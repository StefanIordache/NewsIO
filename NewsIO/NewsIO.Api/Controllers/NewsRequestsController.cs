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
    public class NewsRequestsController : ControllerBase
    {
        public INewsRequestService NewsRequestService { get; set; }

        public NewsRequestsController(INewsRequestService newsRequestService)
        {
            NewsRequestService = newsRequestService;
        }

        // GET - /api/NewsRequests/newdId/{pageSize?}/{pageNo?}
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
    }
}