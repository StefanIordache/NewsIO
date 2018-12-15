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

        // GET - /api/AppUsers/getByEmail/{email}
        [HttpGet("getByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            return NotFound();
        }
    }
}