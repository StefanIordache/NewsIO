using System;
using System.Collections.Generic;
using System.Text;

namespace NewsIO.Data.Models.Application
{
    public class News : Entity
    {
        public string Title { get; set; }

        public string Headline { get; set; }

        public string Content { get; set; }

        public string StorageFolder { get; set; }

        public string ThumbnailUrl { get; set; }

        public string ExternalUrl { get; set; }

        public Category Category { get; set; }

        public DateTime PublishDate { get; set; }

        public int PublishedBy { get; set; }

        public DateTime? LastEditDate { get; set; }

        public int? LastEditBy { get; set; }

        public bool FromRequest { get; set; }

        public NewsRequest NewsRequest { get; set; }
    }
}
