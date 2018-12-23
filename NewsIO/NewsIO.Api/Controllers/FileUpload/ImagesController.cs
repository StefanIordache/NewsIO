using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.Utils.ImageServices.Interfaces;

namespace NewsIO.Api.Controllers.FileUpload
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageHandler ImageHandler;

        public ImagesController(IImageHandler imageHandler)
        {
            ImageHandler = imageHandler;
        }

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            return await ImageHandler.UploadImage(file);
        }
    }
}