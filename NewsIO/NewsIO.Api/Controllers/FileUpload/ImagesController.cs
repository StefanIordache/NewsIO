using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.Utils;
using NewsIO.Api.Utils.ImageServices.Interfaces;
using NewsIO.Data.Models.Application;
using NewsIO.Services.Implementations;
using NewsIO.Services.Intefaces;
using static NewsIO.Api.Utils.Models;

namespace NewsIO.Api.Controllers.FileUpload
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageHandler ImageHandler;

        public INewsService NewsService { get; set; }

        public IImageService ImageService { get; set; }

        public ImagesController(IImageHandler imageHandler, INewsService newsService, IImageService imageService)
        {
            ImageHandler = imageHandler;
            NewsService = newsService;
            ImageService = imageService;
        }

        // POST - /api/Images/uploadToNews/{newsId}/{file}
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost("uploadToNews/{newsId}")]
        public async Task<IActionResult> UploadImage(int newsId, IFormFile file)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                var news = await NewsService.GetByIdAsync<News>(newsId);

                if (news == null)
                {
                    return NotFound();
                }
                if (JwtHelper.CheckIfUserIsModerator(token) && news.PublishedById != JwtHelper.GetUserIdFromJwt(token))
                {
                    return Forbid();
                }

                var imageUrl = await ImageHandler.UploadImage(file);

                if (string.IsNullOrEmpty(imageUrl))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                var image = new Image{
                    News = news,
                    NewsId = newsId,
                    Url = imageUrl
                };

                var imageId = await ImageService.AddAsync(image);

                if (imageId > 0 && !string.IsNullOrEmpty(token))
                {
                    await ImageService.PublishEntity<Image>(imageId, JwtHelper.GetUserIdFromJwt(token), JwtHelper.GetUserNameFromJwt(token));
                }

                return Ok(new Response { Status = ResponseType.Successful });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}