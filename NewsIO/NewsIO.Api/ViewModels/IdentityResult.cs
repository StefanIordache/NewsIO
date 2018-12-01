using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsIO.Api.ViewModels
{
    public class IdentityResult
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set;  }

        public string UserRole { get; set; }
    }
}
