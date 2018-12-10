﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Data.Models.User;

namespace NewsIO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> RoleManager;

        private readonly UserManager<User> UserManager;

        public UsersController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
        }
    }
}