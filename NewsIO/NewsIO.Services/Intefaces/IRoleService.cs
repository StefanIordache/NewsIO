using Microsoft.AspNetCore.Identity;
using NewsIO.Data.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Intefaces
{
    public interface IRoleService
    {
        IEnumerable<IdentityRole> GetAllRoles();

        Task<IdentityRole> GetRoleByIdAsync(string roleId);

        Task<IdentityRole> GetRoleByName(string roleName);

        Task<IdentityRole> GetUserRole(User user);
    }
}
