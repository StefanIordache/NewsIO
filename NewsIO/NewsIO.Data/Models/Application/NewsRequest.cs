﻿using NewsIO.Data.Models.Account;
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

        public DateTime RequestDate { get; set; }

        public int RequestedById { get; set; }

        public DateTime? LastEditDate { get; set; }

        public int? LastEditedyById { get; set; }

        public virtual Category Category { get; set; }
    }
}
