using NewsIO.Data.Models.Account;
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

        public int PublishedById { get; set; }

        public DateTime? LastEditDate { get; set; }

        public int? LastEditedyById { get; set; }

        public virtual IEnumerable<News> News { get; set; }

        public virtual IEnumerable<NewsRequest> NewsRequests { get; set; }
    }
}
