using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsIO.Api.ViewModels
{
    public class NewsRequestViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }
    }
}
