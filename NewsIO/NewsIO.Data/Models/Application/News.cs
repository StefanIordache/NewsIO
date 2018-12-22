using NewsIO.Data.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public virtual Category Category { get; set; }

        public bool FromRequest { get; set; }

        public virtual NewsRequest NewsRequest { get; set; }

        //public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
