using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GrandeTravel.Services;
using GrandeTravel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GrandeTravel
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IRepository<TravelPackage>, BaseRepository<TravelPackage>>();
            services.AddScoped<IRepository<Booking>, BaseRepository<Booking>>();
            services.AddScoped<IRepository<Feedback>, BaseRepository<Feedback>>();
            services.AddScoped<IRepository<TravelProviderProfile>, BaseRepository<TravelProviderProfile>>();
            services.AddScoped<IRepository<CustomerProfile>, BaseRepository<CustomerProfile>>();
            
            services.AddIdentity<MyUser, IdentityRole>
                (
                config =>
                {
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequiredLength = 4;
                    config.Password.RequireDigit = false;
                    config.Cookies.ApplicationCookie.AccessDeniedPath = "/Account/AccessDenied";
                    config.SignIn.RequireConfirmedEmail = true;
                }

                ).AddEntityFrameworkStores<GrandeTravelDbContext>().AddDefaultTokenProviders();
            services.AddDbContext<GrandeTravelDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseIdentity();
            

            app.UseMvcWithDefaultRoute();
        }
    }
}
