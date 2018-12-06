using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.ViewModels;

namespace NewsIO.Api.Controllers
{
    //[Authorize(Policy = "TestPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> RoleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            RoleManager = roleManager;
        }

        // GET - /api/Roles
        [HttpGet]
        public IActionResult GetRolesAsync()
        {
            try
            {
                IEnumerable<IdentityRole> roles;
                IList<RoleViewModel> rolesList = new List<RoleViewModel>();

                roles = RoleManager.Roles.ToList();

                if (roles == null || !roles.Any())
                {
                    return NotFound();
                }

                foreach (var role in roles)
                {
                    RoleViewModel newRole = new RoleViewModel
                    {
                        Id = role.Id,
                        Name = role.NormalizedName
                    };

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
                    return Ok(new RoleViewModel
                    {
                        Id = userRole.Id,
                        Name = userRole.NormalizedName
                    });
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
                    return Ok(new RoleViewModel
                    {
                        Id = userRole.Id,
                        Name = userRole.NormalizedName
                    });
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