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

        //public virtual IEnumerable<News> News { get; set; }

        //public virtual IEnumerable<NewsRequest> NewsRequests { get; set; }
    }
}
