using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsIO.Data.Contexts;
using NewsIO.Data.Models.Account;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Implementations
{
    public class UserService : IUserService
    {
        protected UserContext UserContext { get; set; }

        private UserManager<User> UserManager;

        private RoleManager<IdentityRole> RoleManager;

        private IRoleService RoleService;

        public UserService(UserContext userContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IRoleService roleService)
        {
            UserContext = userContext;
            UserManager = userManager;
            RoleManager = roleManager;
            RoleService = roleService;
        }

        public int CountUsers()
        {
            return UserManager.Users.Count();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await UserManager.Users.ToListAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var userByEmail = await UserManager.FindByEmailAsync(email);

            return userByEmail;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var userById = await UserManager.FindByIdAsync(id);

            return userById;
        }

        public async Task<User> GetByUsernameAsync(string userName)
        {
            var userByUsername = await UserManager.FindByNameAsync(userName);

            return userByUsername;
        }

        public Task<(IEnumerable<User>, int)> GetWithPaginationAsync(int pageSize, int pageNo)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> Set()
        {
            return UserContext.Set<User>();
        }

        public async Task<bool> UpdateAsync(string id, User entry)
        {
            var updateResult = await UserManager.UpdateAsync(entry);

            if (updateResult.Succeeded)
            {
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }

        public async Task<bool> ChangeRole(User user, string roleName)
        {
            if (user != null)
            {
                var newRole = await RoleManager.FindByNameAsync(roleName);

                if (newRole == null)
                {
                    return await Task.FromResult(false);
                }

                var userRoles = await UserManager.GetRolesAsync(user);

                if (userRoles.Any() && userRoles != null)
                {
                    await UserManager.RemoveFromRolesAsync(user, userRoles);
                }

                var userAddedToRoleResult = await UserManager.AddToRoleAsync(user, roleName);

                //Rollback is user role change fails
                if (!userAddedToRoleResult.Succeeded)
                {
                    if (userRoles.Any() && userRoles != null)
                    {
                        await UserManager.AddToRolesAsync(user, userRoles);
                    }

                    return await Task.FromResult(false);
                }

                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }

        public async Task<IdentityRole> GetRole(User user)
        {
            var userRoles = await UserManager.GetRolesAsync(user);

            if (!userRoles.Any() || userRoles == null)
            {
                return null;
            }

            return await RoleService.GetRoleByName(userRoles.First());
        }
    }
}
