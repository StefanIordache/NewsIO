using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsIO.Api.ViewModels
{
    public class AppUserViewModel
    {
        public Guid IdentityId { get; set; }

        public string FacebookId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Location { get; set; }

        public string Gender { get; set; }

        public Guid RoleId { get; set; }

        public string RoleName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool? EmailConfirmed { get; set; }

        public bool? PhoneNumberConfirmed { get; set; }
    }
}
