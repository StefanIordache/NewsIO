using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace NewsIO.Data.Models.User
{
    public class User : IdentityUser<int>
    {
        public string Description { get; set; }

        public UserRole UserRole { get; set; }
    }
}
