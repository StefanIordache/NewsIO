using NewsIO.Data.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NewsIO.Data.Models.Application
{
    public class Comment : Entity
    {
        public string CommentText { get; set; }

        public int NewsId { get; set; }

        public virtual News News { get; set; }
    }
}
