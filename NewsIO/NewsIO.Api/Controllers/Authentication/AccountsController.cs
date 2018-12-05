using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.Extensions;
using NewsIO.Api.ViewModels;
using NewsIO.Data.Contexts;
using NewsIO.Data.Models.User;
using static NewsIO.Api.Utils.Models;

namespace NewsIO.Api.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserContext UserContext;

        private readonly UserManager<User> UserManager;

        private readonly RoleManager<IdentityRole> RoleManager;

        private readonly IMapper Mapper;

        public AccountsController(UserContext userContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            UserContext = userContext;
            UserManager = userManager;
            RoleManager = roleManager;
            Mapper = mapper;
        }

        /*public AccountsController(UserContext userContext, UserManager<User> userManager, IMapper mapper)
        {
            UserContext = userContext;
            UserManager = userManager;
            Mapper = mapper;
        }*/

        // POST /api/accounts/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser([FromBody]RegistrationViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existUser = await UserManager.FindByNameAsync(model.UserName);

                if (existUser != null)
                {
                    return Ok(new Response { Status = ResponseType.Failed, Message = "Username already exist!" });
                }

                var userIdentity = Mapper.Map<User>(model);

                var userCreateResp = await UserManager.CreateAsync(userIdentity, model.Password);

                if (!userCreateResp.Succeeded)
                {
                    return Ok(new Response { Status = ResponseType.Failed, Message = userCreateResp.Errors.FirstOrDefault().Description });
                }

                await UserContext.AppUsers.AddAsync(new AppUser
                {
                    IdentityId = userIdentity.Id,
                    Location = model.Location
                });

                var role = await RoleManager.FindByNameAsync("Member");

                var addedToRoleResp = await UserManager.AddToRoleAsync(userIdentity, "Member");

                await UserContext.SaveChangesAsync();

                return Ok(new Response { Status = ResponseType.Successful });
            }
            catch (Exception e)
            {
                return Ok(new Response { Status = ResponseType.Failed, Message = e.ToString() });
            }

        }
    }
}