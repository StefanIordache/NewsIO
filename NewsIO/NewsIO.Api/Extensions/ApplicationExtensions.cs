using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsIO.Api.Utils;
using NewsIO.Data.Contexts;
using NewsIO.Data.Models.Account;
using NewsIO.Data.Models.Application;
using NewsIO.Services.Implementations;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsIO.Api.Extensions
{
    public static class ApplicationExtensions
    {
        public static async Task<IApplicationBuilder> SeedApplication(this IApplicationBuilder app, IConfiguration configuration)
        {
            try
            {
                IServiceScopeFactory scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

                SeedHelper seedHelper = new SeedHelper();

                using (IServiceScope scope = scopeFactory.CreateScope())
                {
                    RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    ApplicationContext applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                    // Seed users
                    try
                    {
                        var usersSeed = SeedHelper.GetUsers();
                        foreach (var entry in usersSeed)
                        {
                            var user = entry.Key;
                            var roleName = entry.Value;

                            if (roleManager.RoleExistsAsync(roleName).Result
                                && userManager.FindByEmailAsync(user.Email).Result == null
                                && userManager.FindByNameAsync(user.UserName).Result == null)
                            {
                                var userCreateResp = await userManager.CreateAsync(user, "Pa55word");

                                if (userCreateResp.Succeeded)
                                {
                                    var addedToRoleResp = await userManager.AddToRoleAsync(user, roleName);
                                }
                            }
                        }
                    }
                    catch { }

                    //Get admin user for further seed insert
                    var adminUser = userManager.FindByEmailAsync("admin@test.com").Result;

                    //Seed Categories
                    try
                    {
                        ICategoryService categoryService = new CategoryService(applicationContext);

                        var categoriesSeed = SeedHelper.GetCategories();
                        foreach (var entry in categoriesSeed)
                        {
                            if (applicationContext.Categories.FirstOrDefault(c => c.Title.Equals(entry.Title)) == null)
                            {
                                var entryId = await categoryService.AddAsync(entry);

                                if (entryId > 0 && adminUser != null)
                                {
                                    await categoryService.PublishEntity<Category>(entryId, adminUser.Id, adminUser.UserName);
                                }
                            }
                        }
                    }
                    catch { }
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
