using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsIO.Api.Extensions;
using NewsIO.Api.Utils;
using NewsIO.Data.Contexts;
using AutoMapper;
using FluentValidation.AspNetCore;
using System;
using System.IO;
using Microsoft.Extensions.FileProviders;
using NewsIO.Api.Utils.Seed;
using System.Threading.Tasks;
using AspNetCore.RouteAnalyzer;

namespace NewsIO.Api
{
    public class Startup
    {
        public static IConfigurationSection JwtAppSettingOptions { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbServices();

            JwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            services.AddIdentityService();

            services.AddAutoMapper();

            services.AddCookieOptions();
            services.AddOtherServices();
            services.AddCors();

            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddJwtService();

            services.AddAuthorizationPolicyService();

            services.AddRouteAnalyzer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserContext userContext, ApplicationContext applicationContext)
        {
            applicationContext.Database.EnsureCreated();
            userContext.Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();

            ConfigureAsync(app).Wait();

            app.UseCors(builder =>
            {
                builder.AllowCredentials().AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRouteAnalyzer("/routes");
            });

            app.UseCors("AllowSpecificOrigin");
        }

        public async Task ConfigureAsync(IApplicationBuilder app)
        {
            await app.SeedApplication(Configuration).ConfigureAwait(false);

            return;
        }
    }
}
