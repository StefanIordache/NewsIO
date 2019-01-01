using Microsoft.AspNetCore.Authorization;
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

        public INewsService NewsService { get; set; }

        public CommentsController(ICommentService commentsService, INewsService newsService)
        {
            CommentService = commentsService;
            NewsService = newsService;
        }

        // GET - /api/Comments/{newdId}/{pageSize?}/{pageNo?}
        [HttpGet]
        public async Task<IActionResult> GetCommentsAsync(int newsId, int pageSize = 0, int pageNo = 0)
        {
            try
            {
                IEnumerable<Comment> comments;
                PagedResult<Comment> pagedResult = new PagedResult<Comment>();
                int numberOfRecords = 0;

                var news = NewsService.GetByIdAsync<News>(newsId);

                if (news == null)
                {
                    return NotFound();
                }

                if (pageSize > 0)
                {
                    (comments, numberOfRecords) = await CommentService.GetWithPaginationByNewsIdAsync(newsId, pageSize, pageNo);
                }
                else
                {
                    comments = await CommentService.GetAllByNewsIdAsync(newsId);
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
        [Authorize(Roles = "Administrator, Moderator, Member")]
        [HttpPost("add/{newsId}")]
        public async Task<IActionResult> AddNewComment(int newsId, [FromBody] Comment comment)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                var news = await NewsService.GetByIdAsync<News>(newsId);

                if (news == null)
                {
                    return NotFound();
                }

                comment.News = news;

                var entryId = await CommentService.AddAsync(comment);

                if (entryId > 0)
                {
                    await CommentService.PublishEntity<Comment>(entryId, JwtHelper.GetUserIdFromJwt(token), JwtHelper.GetUserNameFromJwt(token));
                }

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

        // PUT - /api/Comments/edit/{commentId}
        [Authorize(Roles = "Administrator, Moderator, Member")]
        [HttpPut("edit/{commentId}")]
        public async Task<IActionResult> EditComment(int commentId, [FromBody] Comment comment)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                var updatedEntry = await CommentService.GetByIdAsync<Comment>(commentId);

                if (updatedEntry == null)
                {
                    return NotFound();
                }
                if (JwtHelper.CheckIfUserIsMember(token) && updatedEntry.PublishedById != JwtHelper.GetUserIdFromJwt(token))
                {
                    return Forbid();
                }
                if (JwtHelper.CheckIfUserIsModerator(token))
                {
                    var news = await NewsService.GetByIdAsync<News>(comment.NewsId);

                    if (news.PublishedById != JwtHelper.GetUserIdFromJwt(token))
                    {
                        return Forbid();
                    }  
                }

                await CommentService.UpdateAsync(commentId, comment);

                if (!string.IsNullOrEmpty(token))
                {
                    await CommentService.UpdateLastEdit<Comment>(commentId, JwtHelper.GetUserIdFromJwt(token), JwtHelper.GetUserNameFromJwt(token));
                }

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

        // DELETE - /api/Comments/delete/{commentId}
        [Authorize(Roles = "Administrator, Moderator, Member")]
        [HttpDelete("delete/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                var deletedEntry = await CommentService.GetByIdAsync<Comment>(commentId);

                if (deletedEntry == null)
                {
                    return NotFound();
                }
                if (JwtHelper.CheckIfUserIsMember(token) && deletedEntry.PublishedById != JwtHelper.GetUserIdFromJwt(token))
                {
                    return Forbid();
                }
                if (JwtHelper.CheckIfUserIsModerator(token))
                {
                    var news = await NewsService.GetByIdAsync<News>(deletedEntry.NewsId);

                    if (news.PublishedById != JwtHelper.GetUserIdFromJwt(token))
                    {
                        return Forbid();
                    }
                }

                await CommentService.Delete<Comment>(commentId);

                return Ok(new Response { Status = ResponseType.Successful });
            }
            catch
            {
                return Ok(new Response { Status = ResponseType.Failed });
            }
        }
    }
}