using NewsIO.Data.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NewsIO.Data.Models.Application
{
    public class NewsRequest : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public bool? IsClosed { get; set; }

        public DateTime? RequestDate { get; set; }

        public string RequestedById { get; set; }

        public string RequestedBy { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
