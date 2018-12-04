using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsIO.Api.Utils;
using NewsIO.Api.Utils.AuthJwtFactory;
using NewsIO.Data.Contexts;
using NewsIO.Data.Models.User;
using NewsIO.Services.Implementations;
using NewsIO.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsIO.Api.Extensions
{
    public static class ServiceExtensions
    {
        private const int statusCodeUnauthorized = 401;

        public static IServiceCollection AddCookieOptions(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(90);
                options.Events.OnRedirectToLogin = context => {
                    context.Response.Clear();
                    context.Response.StatusCode = statusCodeUnauthorized;
                    return Task.FromResult(0);
                };
            });

            return services;
        }

        public static IServiceCollection AddDbServices(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NewsIOApplication;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            });
            services.AddDbContext<UserContext>(options =>
            {
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NewsIOUsers;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            });
            services
                .AddTransient<ICategoryService, CategoryService>();

            return services;
        }
    }
}
