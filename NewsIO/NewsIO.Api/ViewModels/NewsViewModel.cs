using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.Utils;
using NewsIO.Data.Models.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsIO.Api.ViewModels
{
    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "entry")]
    public class NewsViewModel
    {
        public string Title { get; set; }

        public string Headline { get; set; }

        public string Content { get; set; }

        public string ThumbnailUrl { get; set; }

        public string ExternalUrl { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public bool? FromRequest { get; set; }

        public int? NewsRequestId { get; set; }

        public virtual NewsRequest NewsRequest { get; set; }

        public IFormFile Thumbnail { get; set; }
    }
}
