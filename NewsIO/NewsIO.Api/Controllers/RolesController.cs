using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.ViewModels;
using NewsIO.Services.Intefaces;

namespace NewsIO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> RoleManager;

        public IRoleService RoleService { get; set; }

        private readonly IMapper Mapper;

        public RolesController(RoleManager<IdentityRole> roleManager, IRoleService roleService, IMapper mapper)
        {
            RoleManager = roleManager;
            RoleService = roleService;
            Mapper = mapper;
        }

        // GET - /api/Roles
        [HttpGet]
        public IActionResult GetRolesAsync()
        {
            try
            {
                IEnumerable<IdentityRole> roles;
                IList<RoleViewModel> rolesList = new List<RoleViewModel>();

                roles = RoleService.GetAllRoles();

                if (roles == null || !roles.Any())
                {
                    return NotFound();
                }

                foreach (var role in roles)
                {
                    var newRole = Mapper.Map<RoleViewModel>(role);

                    rolesList.Add(newRole);
                }

                return Ok(rolesList);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET - /api/Roles/getById/{id}
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            try
            {
                IdentityRole userRole = await RoleManager.FindByIdAsync(id.ToString());

                if (userRole != null)
                {
                    var result = Mapper.Map<RoleViewModel>(userRole);

                    return Ok(result);
                }

                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET - /api/Roles/getByName/{id}
        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            try
            {
                IdentityRole userRole = await RoleManager.FindByNameAsync(name);

                if (userRole != null)
                {
                    var result = Mapper.Map<RoleViewModel>(userRole);

                    return Ok(result);
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