﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.SpaServices;
using NewsIO.Api.Extensions;
using NewsIO.Api.Utils;
using NewsIO.Data.Contexts;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NewsIO.Data.Models.User;
using NewsIO.Api.Utils.AuthJwtFactory;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using FluentValidation.AspNetCore;

namespace NewsIO.Api
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure

        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbServices();

            services.AddSingleton<IJwtFactory, JwtFactory>();

            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            // Jwt wire up
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions)); // todo: to be moved

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

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
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // Api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorPolicy", policy => policy.RequireAssertion(context => context.User.IsInRole("Administrator")));
                options.AddPolicy("ModeratorPolicy", policy => policy.RequireAssertion(context => context.User.IsInRole("Moderator")));
                options.AddPolicy("MemberPolicy", policy => policy.RequireAssertion(context => context.User.IsInRole("Member")));
            });

            // Add identity
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

            services.AddAutoMapper();

            services.AddCookieOptions();
            services.AddCors();

            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, UserContext userContext, ApplicationContext applicationContext)
        {
            userContext.Database.EnsureCreated();
            applicationContext.Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            await app.EnsureRolesCreatedAsync(Configuration);

            app.UseCors(builder =>
            {
                builder.AllowCredentials().AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
            app.UseCors("AllowSpecificOrigin");
        }
    }
}
