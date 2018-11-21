using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsIO.Api.Extensions
{
    public static class AccountExtensions
    {
        /*public static async Task<SignInResult> PasswordEmailSignInAsync(this SignInManager<User> signInManager, string email, string password, bool isPersistent, bool shouldLockout, UserManager<User> userManager)
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
                    RoleManager<UserRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<UserRole>>();

                    var roles = configuration.GetSection("Roles").AsEnumerable();
                    foreach (var role in roles)
                        if (role.Value != null && !roleManager.RoleExistsAsync(role.Value).Result)
                        {
                            var adminRole = new UserRole(role.Value);
                            await roleManager.CreateAsync(adminRole);
                        }
                }
                return app;
            }
            catch (Exception e)
            {
                return app;
            }
        }*/
    }
}
