using System;
using System.Collections.Generic;
using System.Text;

namespace NewsIO.Data.Models.Application
{
    public class NewsRequest : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime RequestDate { get; set; }

        public int RequestedBy { get; set; }

        public Category Category { get; set; }


    }
}
