using NewsIO.Data.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NewsIO.Data.Models.Application
{
    public class Comment : Entity
    {
        public string CommentText { get; set; }

        public virtual News News { get; set; }

        public DateTime PublishDate { get; set; }

        public AppUser PublishedBy { get; set; }

        public DateTime? LastEditDate { get; set; }

        public virtual AppUser LastEditedyBy { get; set; }
    }
}
