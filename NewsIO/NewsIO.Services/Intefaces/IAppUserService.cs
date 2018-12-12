using Microsoft.AspNetCore.Identity;
using NewsIO.Data.Models;
using NewsIO.Data.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Services.Intefaces
{
    public interface IAppUserService : IGenericDbService
    {
        Task<User> GetByEmailAsync(string email);
    }
}
