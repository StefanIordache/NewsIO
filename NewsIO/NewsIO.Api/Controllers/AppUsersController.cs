using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.Utils;
using NewsIO.Api.ViewModels;
using NewsIO.Data.Models.User;
using NewsIO.Services.Intefaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsIO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        public IAppUserService AppUserService { get; set; }

        public IRoleService RoleService { get; set; }

        private readonly RoleManager<IdentityRole> RoleManager;

        private readonly UserManager<User> UserManager;

        private readonly IMapper Mapper;

        public AppUsersController(IAppUserService appUserService, IRoleService roleService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IMapper mapper)
        {
            AppUserService = appUserService;
            RoleService = roleService;
            RoleManager = roleManager;
            UserManager = userManager;
            Mapper = mapper;
        }

        // GET - /api/AppUsers/{pageSize?}/{pageNo?}
        [HttpGet]
        public async Task<IActionResult> GetAppUsersAsync(int pageSize = 0, int pageNo = 0)
        {
            try
            {
                IEnumerable<AppUser> appUsers;
                IList<AppUserViewModel> appUsersVM = new List<AppUserViewModel>();
                PagedResult<AppUser> pagedResult = new PagedResult<AppUser>();
                int numberOfRecords = 0;

                if (pageSize > 0)
                {
                    (appUsers, numberOfRecords) = await AppUserService.GetWithPaginationAsync<AppUser>(pageSize, pageNo);
                }
                else
                {
                    appUsers = await AppUserService.GetAllAsync<AppUser>();
                }

                if (appUsers == null || !appUsers.Any())
                {
                    return NotFound();
                }

                foreach (var appUser in appUsers)
                {
                    var appUserVM = Mapper.Map<AppUser, AppUserViewModel>(appUser);
                    Mapper.Map(appUser.Identity, appUserVM);

                    var appUserRole = await RoleService.GetUserRole(appUser.Identity);
                    Mapper.Map(appUserRole, appUserVM);

                    appUsersVM.Add(appUserVM);
                }

                return Ok(appUsersVM);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}