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

        public static async Task<IApplicationBuilder> EnsureRolesCreatedAsync(this IApplicationBuilder app, IConfiguration configuration)
        {
            try
            {
                IServiceScopeFactory scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

                using (IServiceScope scope = scopeFactory.CreateScope())
                {
                    RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    var roles = configuration.GetSection("Roles").AsEnumerable();
                    foreach (var role in roles)
                        if (role.Value != null && !roleManager.RoleExistsAsync(role.Value).Result)
                        {
                            var newRole = new IdentityRole(role.Value);
                            await roleManager.CreateAsync(newRole);
                        }
                }
                return app;
            }
            catch (Exception e)
            {
                return app;
            }
        }
    }
}
