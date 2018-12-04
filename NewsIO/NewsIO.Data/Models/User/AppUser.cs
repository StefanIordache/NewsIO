using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace NewsIO.Data.Models.User
{
    public class AppUser : Entity
    {
        public string IdentityId { get; set; }

        public User Identity { get; set; }

        public string Location { get; set; }

        public string Gender { get; set; }
    }
}
