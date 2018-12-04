using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Data.Models.User;

namespace NewsIO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RoleManager<UserRole> RoleManager;

        private readonly UserManager<User> UserManager;

        public UsersController(RoleManager<UserRole> roleManager, UserManager<User> userManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
        }
    }
}