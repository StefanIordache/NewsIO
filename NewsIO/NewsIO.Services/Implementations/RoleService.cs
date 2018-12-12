using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsIO.Data.Contexts;
using NewsIO.Data.Models.User;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Implementations
{
    public class RoleService : IRoleService
    {
        protected UserContext UserContext { get; set; }

        private readonly RoleManager<IdentityRole> RoleManager;

        private readonly UserManager<User> UserManager; 

        public RoleService(UserContext userContext, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            UserContext = userContext;
            RoleManager = roleManager;
            UserManager = userManager;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            IEnumerable<IdentityRole> roles = RoleManager.Roles.ToList();

            return roles;
        }

        public async Task<IdentityRole> GetRoleByIdAsync(string roleId)
        {
            return await RoleManager.FindByIdAsync(roleId);
        }

        public async Task<IdentityRole> GetRoleByName(string roleName)
        {
            return await RoleManager.FindByNameAsync(roleName);
        }

        public async Task<IdentityRole> GetUserRole(User user)
        {
            var userRoles = await UserManager.GetRolesAsync(user);
            return await GetRoleByName(userRoles.First());
        }
    }
}
