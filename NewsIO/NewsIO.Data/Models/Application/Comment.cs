using System;
using System.Collections.Generic;
using System.Text;

namespace NewsIO.Data.Models.Application
{
    public class Comment : Entity
    {
        public string CommentText { get; set; }

        public News News { get; set; }

        public DateTime PublishDate { get; set; }

        public int PublishedBy { get; set; }

        public DateTime? LastEditDate { get; set; }

        public int LastEditBy { get; set; }
    }
}
