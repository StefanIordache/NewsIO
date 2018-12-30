using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsIO.Api.Utils.Seed;
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
        public static async Task SeedApplication(this IApplicationBuilder app, IConfiguration configuration)
        {
            try
            {
                IServiceScopeFactory scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();



                using (IServiceScope scope = scopeFactory.CreateScope())
                {
                    RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    ApplicationContext applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                    ICategoryService categoryService = new CategoryService(applicationContext);
                    INewsRequestService newsRequestService = new NewsRequestService(applicationContext);

                    // Seed roles
                    try
                    {
                        var rolesSeed = SeedHelper.GetRoles();
                        foreach (var role in rolesSeed)
                            if (!string.IsNullOrEmpty(role) && !roleManager.RoleExistsAsync(role).Result)
                            {
                                var newRole = new IdentityRole(role);
                                await roleManager.CreateAsync(newRole);
                            }
                    }
                    catch { }

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
                                var userCreateResp = userManager.CreateAsync(user, "Pa55word");

                                if (userCreateResp.Result.Succeeded)
                                {
                                    await userManager.AddToRoleAsync(user, roleName);
                                }
                            }
                        }
                    }
                    catch { }

                    //Get users for further seed insert
                    var admin = userManager.FindByEmailAsync("admin@test.com").Result;
                    var member = userManager.FindByEmailAsync("member@test.com").Result;
                    var moderator = userManager.FindByEmailAsync("moderator@test.com").Result;

                    //Seed Categories
                    try
                    {
                        var categoriesSeed = SeedHelper.GetCategories();
                        foreach (var entry in categoriesSeed)
                        {
                            if (applicationContext.Categories.FirstOrDefault(c => c.Title.Equals(entry.Title)) == null)
                            {
                                var entryId = categoryService.AddAsync(entry);

                                if (entryId.Result > 0 && admin != null)
                                {
                                    await categoryService.PublishEntity<Category>(entryId.Result, admin.Id, admin.UserName);
                                }
                            }
                        }
                    }
                    catch { }

                    //Seed NewsRequests
                    try
                    {
                        var newsRequestsSeed = SeedHelper.GetNewsRequests();
                        foreach (var entry in newsRequestsSeed)
                        {
                            var newsRequest = entry.Key;
                            var categoryTitle = entry.Value;

                            var category = categoryService.GetByTitle(categoryTitle);

                            if (category != null && applicationContext.NewsRequests.FirstOrDefault(nr => nr.Title.Equals(newsRequest.Title)) == null)
                            {
                                newsRequest.Category = category;
                                newsRequest.RequestDate = DateTime.Now;
                                newsRequest.RequestedBy = member.UserName;
                                newsRequest.RequestedById = member.Id;

                                var entryId = newsRequestService.AddAsync(newsRequest);

                                if (entryId.Result > 0 && moderator != null)
                                {
                                    await newsRequestService.PublishEntity<NewsRequest>(entryId.Result, moderator.Id, moderator.UserName);
                                }
                            }
                        }
                    }
                    catch
                    { }
                }

                return;
            }
            catch (Exception e)
            {
                return;
            }
        }
    }
}
