using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.Utils;
using NewsIO.Api.ViewModels;
using NewsIO.Data.Models.Account;
using NewsIO.Services.Intefaces;
using static NewsIO.Api.Utils.Models;

namespace NewsIO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IRoleService RoleService { get; set; }

        public IUserService UserService { get; set; }

        private readonly RoleManager<IdentityRole> RoleManager;

        private readonly UserManager<User> UserManager;

        private readonly IMapper Mapper;

        public UsersController(IRoleService roleService, IUserService userService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IMapper mapper)
        {
            RoleService = roleService;
            UserService = userService;
            RoleManager = roleManager;
            UserManager = userManager;
            Mapper = mapper;
        }

        // GET - /api/Users/{pageSize?}/{pageNo?}
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync(int pageSize = 0, int pageNo = 0)
        {
            try
            {
                IEnumerable<User> users;
                IList<AppUserViewModel> usersVM = new List<AppUserViewModel>();
                PagedResult<User> pagedResult = new PagedResult<User>();
                int numberOfRecords = 0;

                if (pageSize > 0)
                {
                    (users, numberOfRecords) = await UserService.GetWithPaginationAsync(pageSize, pageNo);
                }
                else
                {
                    users = await UserService.GetAllAsync();
                }

                if (users == null || !users.Any())
                {
                    return NotFound();
                }

                foreach (var user in users)
                {
                    var userRole = await RoleService.GetUserRole(user);

                    var userVM = Mapper.Map<User, AppUserViewModel>(user);
                    Mapper.Map(userRole, userVM);

                    usersVM.Add(userVM);
                }

                return Ok(usersVM);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET - /api/Users/getByEmail/{email}
        [HttpGet("getByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmailAsync(string email)
        {
            try
            {
                User user;

                user = await UserService.GetByEmailAsync(email);

                if (user != null)
                {
                    var userVM = Mapper.Map<User, AppUserViewModel>(user);

                    return Ok(userVM);
                }

                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // GET - /api/Users/getById/{id}
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                User user;

                user = await UserService.GetByIdAsync(id);

                if (user != null)
                {
                    var userVM = Mapper.Map<User, AppUserViewModel>(user);

                    return Ok(userVM);
                }

                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // POST - /api/Users/changeRole/{identity}
        [HttpPost("changeRole/{identity}/{roleName}")]
        public async Task<IActionResult> ChangeUserRoleAsync(string identity, string roleName)
        {
            try
            {
                User user;

                user = await UserService.GetByIdAsync(identity);

                //Try and get user by email
                if (user == null)
                {
                    user = await UserService.GetByEmailAsync(identity);
                }

                if (user != null)
                {
                    var roleChanged = await UserService.ChangeRoleAsync(user, roleName);

                    if (roleChanged )
                    {
                        return Ok(new Response { Status = ResponseType.Successful });
                    }

                    return Ok(new Response { Status = ResponseType.Failed });
                }

                return NotFound();
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}