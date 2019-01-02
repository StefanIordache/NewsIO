using System;
using System.Collections.Generic;
using System.Text;

namespace NewsIO.Data.Models.Application
{
    public class Image : Entity
    {
        public string Url { get; set; }

        public int NewsId { get; set; }

        public News News { get; set; }
    }
}
