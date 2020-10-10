using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AnnoBibLibrary.Models;
using AnnoBibLibrary.Repos;
using AnnoBibLibrary.Services;
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
                            builder.AllowAnyMethod();
                            builder.AllowAnyHeader();
                            builder.AllowCredentials();
                        });
                });

            services.AddDbContext<AppDbContext>(options => 
                options.UseMySql(Configuration.GetConnectionString("LibraryDb"), options => {
                    options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                })
            );

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(Configuration.GetSection("IdentityOptions"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddControllers();

            services.AddScoped<IUserRepo, DbUserRepo>();
            services.AddScoped<ISourceRepo, DbSourceRepo>();
            services.AddScoped<ILibraryRepo, DbLibraryRepo>();
            services.AddScoped<IAnnotationRepo, DbAnnotationRepo>();
            services.AddScoped<IAnnotationLinkRepo, DbAnnotationLinkRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("AllowVueClient");
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection(); Eventually add this when public

            app.UseAuthentication();

            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
