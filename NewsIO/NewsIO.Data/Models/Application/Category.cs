using NewsIO.Data.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NewsIO.Data.Models.Application
{
    public class Category : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishDate { get; set; }

        public AppUser PublishedBy { get; set; }

        public DateTime? LastEditDate { get; set; }

        public virtual AppUser LastEditedyBy { get; set; }

        public virtual IEnumerable<News> News { get; set; }

        public virtual IEnumerable<NewsRequest> NewsRequests { get; set; }
    }
}
