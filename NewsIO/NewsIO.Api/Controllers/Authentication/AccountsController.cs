using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.Extensions;
using NewsIO.Api.ViewModels;
using NewsIO.Data.Models.User;
using static NewsIO.Api.Utils.Models;

namespace NewsIO.Api.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> UserManager;

        private readonly SignInManager<User> SignInManager;

        private readonly RoleManager<UserRole> RoleManager;

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManger, RoleManager<UserRole> roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManger;
            RoleManager = roleManager;
        }

        [HttpPost("identity")]
        public async Task<IActionResult> Identity()
        {
            try
            {
                var account = UserManager.GetUserAsync(User).Result;
                Console.WriteLine("----------------Hellooooo------------------------");
                Console.WriteLine(account);
                Console.WriteLine("----------------BYEEE------------------------");
                var userRoles = await UserManager.GetRolesAsync(account);

                if (account != null)
                {
                    return Ok(new ViewModels.IdentityResult
                    {
                        Id = account.Id,
                        UserName = account.UserName,
                        Email = account.Email,
                        UserRole = userRoles.First()
                       
                    });
                }

                return Unauthorized();
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPost("signIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody]SignIn model)
        {
            try
            {
                Response response = new Response { Status = ResponseType.Failed };
                if (ModelState.IsValid)
                {
                    var result = await SignInManager.PasswordEmailSignInAsync(model.Email, model.Password, true, false, UserManager);
                    if (result.Succeeded)
                    {
                        response.Status = ResponseType.Successful;
                    }
                    else
                    {
                        response.Message = "Invalid Credentials!";
                    }
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logOut")]
        public async Task<IActionResult> SignOut()
        {
            try
            {
                await SignInManager.SignOutAsync();
                return Ok(new Response { Status = ResponseType.Successful });
            }
            catch (Exception e)
            {
                return Ok(new Response { Status = ResponseType.Failed, Message = e.ToString() });
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]Register model)
        {
            try
            {
                var existUser = UserManager.FindByNameAsync(model.UserName).Result;
                if (existUser != null)
                {
                    return Ok(new Response { Status = ResponseType.Failed, Message = "Username already exist!" });
                }

                var newUser = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true,
                };

                var userCreateResp = await UserManager.CreateAsync(newUser, model.Password);
                

                if (!userCreateResp.Succeeded)
                { 
                    return Ok(new Response { Status = ResponseType.Failed, Message = userCreateResp.Errors.FirstOrDefault().Description });
                }

                var userAddToRoleResp = await UserManager.AddToRoleAsync(newUser, "User");

                if (!userAddToRoleResp.Succeeded)
                {
                    return Ok(new Response { Status = ResponseType.Failed, Message = userAddToRoleResp.Errors.FirstOrDefault().Description });
                }

                return Ok(new Response { Status = ResponseType.Successful });
            }
            catch (Exception e)
            {
                return Ok(new Response { Status = ResponseType.Failed, Message = e.ToString() });
            }
        }

        [HttpPost("changeRole")]
        public async Task<IActionResult> ChangeUserRole([FromBody] UserRoleRequest userRoleRequest)
        {
            try
            {
                var user = await UserManager.FindByIdAsync(userRoleRequest.UserId.ToString());

                if (user == null)
                    return NotFound();

                var userRoles = await UserManager.GetRolesAsync(user);

                if (userRoles == null || !userRoles.Any())
                {
                    return Ok(new Response { Status = ResponseType.Failed, Message = "Failed to retrieve user role!" });
                }

                var removeFromRolesResp =  await UserManager.RemoveFromRolesAsync(user, userRoles);

                if (!removeFromRolesResp.Succeeded)
                {
                    return Ok(new Response { Status = ResponseType.Failed, Message = removeFromRolesResp.Errors.FirstOrDefault().Description });
                }

                var userAddToRoleResp = await UserManager.AddToRoleAsync(user, userRoleRequest.RoleName);

                if (!userAddToRoleResp.Succeeded)
                {
                    return Ok(new Response { Status = ResponseType.Failed, Message = userAddToRoleResp.Errors.FirstOrDefault().Description });
                }

                return Ok(new Response { Status = ResponseType.Successful, Message = "User role changed!" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}