using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsIO.Api.Utils;
using NewsIO.Data.Contexts;
using NewsIO.Data.Models.Account;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewsIO.Api.Extensions
{
    public static class UserExtensions
    {
        public static async Task<SignInResult> PasswordEmailSignInAsync(this SignInManager<User> signInManager, string email, string password, bool isPersistent, bool shouldLockout, UserManager<User> userManager)
        {
            var user = await userManager.FindByEmailAsync(email);
            return await signInManager.PasswordSignInAsync(user.UserName, password, isPersistent, shouldLockout);
        }
    }
}
