using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.Utils.ImageServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsIO.Api.Utils.ImageServices.Implementations
{
    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter ImageWriter;

        public ImageHandler(IImageWriter imageWriter)
        {
            ImageWriter = imageWriter;
        }

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var result = await ImageWriter.UploadImage(file);
            return new ObjectResult(result);
        }
    }
}
