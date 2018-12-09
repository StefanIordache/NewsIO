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
            services.AddCors();

            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddJwtService();

            services.AddAuthorizationPolicyService();
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

            await app.EnsureRolesCreatedAsync(Configuration);

            app.UseCors(builder =>
            {
                builder.AllowCredentials().AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });

            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();
            app.UseCors("AllowSpecificOrigin");
        }
    }
}
