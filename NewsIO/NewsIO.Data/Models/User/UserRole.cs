using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsIO.Data.Models.User
{
    public class UserRole : IdentityRole<int>
    {
        public UserRole() : base() { }

        public UserRole(string roleName) : base(roleName) { }
    }
}
