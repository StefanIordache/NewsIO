using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.Utils;
using NewsIO.Data.Models.Application;
using NewsIO.Services.Intefaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NewsIO.Api.Utils.Models;

namespace NewsIO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        public ICommentService CommentService { get; set; }

        public CommentsController(ICommentService commentsService)
        {
            CommentService = commentsService;
        }

        // GET - /api/Categories/newdId/{pageSize?}/{pageNo?}
        [HttpGet]
        public async Task<IActionResult> GetCommentsAsync(int newsId, int pageSize = 0, int pageNo = 0)
        {
            try
            {
                IEnumerable<Comment> comments;
                PagedResult<Comment> pagedResult = new PagedResult<Comment>();
                int numberOfRecords = 0;

                if (pageSize > 0)
                {
                    (comments, numberOfRecords) = await CommentService.GetWithPaginationAsync<Comment>(pageSize, pageNo);
                }
                else
                {
                    comments = await CommentService.GetAllAsync<Comment>();
                }

                if (comments == null || !comments.Any())
                {
                    return NotFound();
                }

                return Ok(comments);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET - /api/Comments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            try
            {
                Comment comment = await CommentService.GetByIdAsync<Comment>(id);

                if (comment != null)
                {
                    return Ok(comment);
                }

                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST - /api/Comments/add
        [HttpPost("add")]
        public async Task<IActionResult> AddNewComment([FromBody] Comment comment)
        {
            try
            {
                await CommentService.AddAsync(comment);
                return Ok(new Response
                {
                    Status = ResponseType.Successful,
                    Value = comment
                });

            }
            catch
            {
                return Ok(new Response { Status = ResponseType.Failed });
            }
        }


    }
}