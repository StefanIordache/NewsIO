using Microsoft.AspNetCore.Http;
using NewsIO.Api.Utils.ImageServices.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewsIO.Api.Utils.ImageServices.Implementations
{
    public class ImageWriter : IImageWriter
    {
        public async Task<string> UploadImage(IFormFile file)
        {
            if (CheckIfImageFile(file))
            {
                return await WriteFile(file);
            }

            return "Invalid image file";
        }

        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return ImageHelper.GetImageFormat(fileBytes) != ImageHelper.ImageFormat.unknown;
        }

        public async Task<string> WriteFile(IFormFile file)
        {
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

                fileName = Guid.NewGuid().ToString() + extension; 

                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images"));
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return fileName;
        }
    }
}
