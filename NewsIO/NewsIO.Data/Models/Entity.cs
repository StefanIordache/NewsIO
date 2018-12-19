using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsIO.Data.Models
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }

        public DateTime? PublishDate { get; set; }

        public string PublishedById { get; set; }

        public string PublishedBy { get; set; }

        public DateTime? LastEditDate { get; set; }

        public string LastEditedyById { get; set; }

        public string LastEditedBy { get; set; }
    }
}
