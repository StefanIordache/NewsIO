using System;
using System.Collections.Generic;
using System.Text;

namespace NewsIO.Data.Models.Application
{
    class Media : Entity
    {
        public string Url { get; set; }

        public bool IsNewsThumbnail { get; set; }

        public bool IsNewsPicture { get; set; }

        public bool IsCategoryThumbnail { get; set; }
    }
}
