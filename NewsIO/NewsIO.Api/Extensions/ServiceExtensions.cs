using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using NewsIO.Api.Utils;
using NewsIO.Api.Utils.AuthJwtFactory;
using NewsIO.Data.Contexts;
using NewsIO.Data.Models.User;
using NewsIO.Services.Implementations;
using NewsIO.Services.Intefaces;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NewsIO.Api.Extensions
{
    public static class ServiceExtensions
    {
        private const int statusCodeUnauthorized = 401;

        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure

        private readonly static SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

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

        public static IServiceCollection AddJwtService(this IServiceCollection services)
        {
            services.AddSingleton<IJwtFactory, JwtFactory>();

            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = Startup.JwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = Startup.JwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Startup.JwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = Startup.JwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = Startup.JwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            return services;
        }

        public static IServiceCollection AddAuthorizationPolicyService(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("MemberRolePolicy", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(ClaimTypes.Role);
                    policy.RequireRole("Member");
                });
                options.AddPolicy("ModeratorRolePolicy", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(ClaimTypes.Role);
                    policy.RequireRole("Moderator");
                });
                options.AddPolicy("AdministratorRolePolicy", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(ClaimTypes.Role);
                    policy.RequireRole("Administrator");
                });
            });

            return services;
        }

        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(u =>
            {
                // Configure identity options
                u.Password.RequireDigit = false;
                u.Password.RequireLowercase = false;
                u.Password.RequireUppercase = false;
                u.Password.RequireNonAlphanumeric = false;
                u.Password.RequiredLength = 6;

                // Lockout settins
                u.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                u.Lockout.MaxFailedAccessAttempts = 10;
                u.Lockout.AllowedForNewUsers = true;

                // User settings
                u.User.RequireUniqueEmail = true;
            });

            builder.AddEntityFrameworkStores<UserContext>().AddDefaultTokenProviders();

            return services;
        }
    }
}
