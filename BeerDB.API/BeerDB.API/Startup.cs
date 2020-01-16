using System;
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
using Microsoft.EntityFrameworkCore;
using BeerDB.API.Models;
using BeerDB.Models.Repositories;
using BeerDB.Models;
using Microsoft.AspNetCore.Identity;
using BeerDB.Models.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace BeerDB.API
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<BeerDBAPIContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BeerDBAPIContext")));

            services.AddIdentity<BeerDbUser, IdentityRole>()
                .AddEntityFrameworkStores<BeerDBAPIContext>()
                .AddDefaultTokenProviders();

            services.AddCors();

            services.AddAuthentication("HowestScheme")
             .AddCookie("HowestScheme", options =>
             {
                 options.Events =
                 new CookieAuthenticationEvents()
                 {
                     OnRedirectToLogin = (ctx) =>
                     {
                         if (ctx.Request.Path.StartsWithSegments("/api") &&
                     ctx.Response.StatusCode == 200) //redirect is 200
                     {
                         //doe geen redirect naar een loginpagina bij een api call
                         //maar geef een 401 - unauthorized
                         ctx.Response.StatusCode = 401;
                     }
                         return Task.CompletedTask;
                     },
                     OnRedirectToAccessDenied = (ctx) =>
                     {
                         if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                         {
                             ctx.Response.StatusCode = 403; //uitvoering refused
                         }
                         return Task.CompletedTask;
                     }
                 };
             });

            services.AddMvc(options => options.Filters.Add(new AuthorizeFilter()));


            services.AddScoped<ICommentRepo, CommentRepo>();
            services.AddScoped<IAuthenticatieRepo, AuthenticatieRepo>();

            services.AddTransient<SeedIdentity>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedIdentity seedIdentity)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(cfg =>
            {
                cfg.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            });
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
            seedIdentity.SeedIdentityMobileAppsAPI().Wait();
        }
    }
}
