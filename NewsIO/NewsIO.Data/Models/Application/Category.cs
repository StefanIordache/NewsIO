using System;
using System.Collections.Generic;
using System.Text;

namespace NewsIO.Data.Models.Application
{
    public class Category : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
