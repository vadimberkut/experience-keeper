﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExperienceKeeper.Data.DbContexts;
using ExperienceKeeper.Data.Models.Identity;
using ExperienceKeeper.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExperienceKeeper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Use PostgreSQL Database
            string connectionString = Configuration.GetConnectionString("DataAccessPostgreSqlProvider");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(
                    connectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name)
                    );
            });

            // Add Indentity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Services
            services.AddTransient<IEmailSender, EmailSender>();

            // Includes support for Razor Pages and controllers
            services.AddMvc(
                options =>
                {
                }    
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Static files
            app.UseStaticFiles();

            // Use Auth
            app.UseAuthentication();

            // Mvc
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
