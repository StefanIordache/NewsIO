using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsIO.Api.ViewModels;
using NewsIO.Data.Models.User;

namespace NewsIO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<UserRole> RoleManager;

        public RolesController(RoleManager<UserRole> roleManager)
        {
            RoleManager = roleManager;
        }

        // GET - /api/Roles
        [HttpGet]
        public IActionResult GetRolesAsync()
        {
            try
            {
                IEnumerable<UserRole> roles;
                IList<Role> rolesList = new List<Role>();

                roles = RoleManager.Roles.ToList();

                if (roles == null || !roles.Any())
                {
                    return NotFound();
                }

                foreach (var role in roles)
                {
                    Role newRole = new Role
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
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                UserRole userRole = await RoleManager.FindByIdAsync(id.ToString());

                if (userRole != null)
                {
                    return Ok(new Role
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
                UserRole userRole = await RoleManager.FindByNameAsync(name);

                if (userRole != null)
                {
                    return Ok(new Role
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