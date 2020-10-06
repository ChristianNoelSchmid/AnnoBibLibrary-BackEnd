using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AnnoBibLibrary.Models;
using AnnoBibLibrary.Repos;
using AnnoBibLibrary.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AnnoBibLibrary
{
    public class Startup
    {
        public const string CookieAuthName = "CookieAuthScheme";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            services.AddCors(
                options => 
                {
                    options.AddPolicy("AllowVueClient", 
                        builder => 
                        {
                            builder.WithOrigins("http://localhost:8080");
                            builder.AllowAnyHeader();
                            builder.AllowCredentials();
                        });
                });

            services.AddDbContextPool<AppDbContext>(options => 
                options.UseMySql(Configuration.GetConnectionString("LibraryDb"))
            );

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(Configuration.GetSection("IdentityOptions"));

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options => {
                    // Change SameSite settings to ensure that client webapp
                    // can connect when both are on the same server
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = redirectContext =>
                        {
                            redirectContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            return Task.CompletedTask;
                        }
                    };
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                });

            services.AddControllers();

            services.AddScoped<ISourceRepo, DbSourceRepo>();
            services.AddScoped<ILibraryRepo, DbLibraryRepo>();
            services.AddScoped<IAnnotationRepo, DbAnnotationRepo>();
            services.AddScoped<IAnnotationLinkRepo, DbAnnotationLinkRepo>();
            services.AddScoped<Services.IAuthenticationService, Services.AuthenticationService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("AllowVueClient");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // This is the middleware that extracts user information from the request 
            // (using the configured scheme), enabling the application to perform authentication challenges, 
            // for example when adding the [Authorize] attribute. 
            // source: https://www.dotnetcurry.com/aspnet-core/1511/authentication-aspnetcore-signalr-vuejs
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
