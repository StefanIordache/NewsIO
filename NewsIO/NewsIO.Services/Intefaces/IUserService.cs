using Microsoft.AspNetCore.Identity;
using NewsIO.Data.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Intefaces
{
    public interface IUserService
    {
        int CountUsers();

        Task<IEnumerable<User>> GetAllAsync();

        Task<(IEnumerable<User>, int)> GetWithPaginationAsync(int pageSize, int pageNo);

        IQueryable<User> Set();

        Task<User> GetByIdAsync(string id);

        Task<User> GetByEmailAsync(string email);

        Task<User> GetByUsernameAsync(string username);

        Task<bool> UpdateAsync(string id, User entry);

        Task<bool> ChangeRole(User user, string roleName);

        Task<IdentityRole> GetRole(User user);
    }
}
