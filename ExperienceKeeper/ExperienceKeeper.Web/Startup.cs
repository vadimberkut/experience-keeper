using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExperienceKeeper.Data.DbContexts;
using ExperienceKeeper.Data.Extensions;
using ExperienceKeeper.Entity.Entities.Identity;
using ExperienceKeeper.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }

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
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Config Identity
                if (Env.IsDevelopment())
                {
                    options.Password = new PasswordOptions
                    {
                        RequireDigit = false,
                        RequiredUniqueChars = 0,
                        RequireLowercase = false,
                        RequireUppercase = false,
                        RequireNonAlphanumeric = false,
                        RequiredLength = 6
                    };
                    options.Lockout = new LockoutOptions
                    {
                        AllowedForNewUsers = true,
                        DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30),
                        MaxFailedAccessAttempts = 3
                    };
                }
                else
                {
                    options.Password = new PasswordOptions
                    {
                        RequireDigit = true,
                        RequiredUniqueChars = 4,
                        RequireLowercase = true,
                        RequireUppercase = true,
                        RequireNonAlphanumeric = true,
                        RequiredLength = 6
                    };
                    options.Lockout = new LockoutOptions
                    {
                        AllowedForNewUsers = true,
                        DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),
                        MaxFailedAccessAttempts = 5
                    };
                }
                options.SignIn = new SignInOptions
                {
                    RequireConfirmedEmail = false,
                    RequireConfirmedPhoneNumber = false
                };
                options.User = new UserOptions
                {
                    RequireUniqueEmail = true,
                    AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"
                };
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Config Cookies
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookie.Expiration = TimeSpan.FromDays(14);
                options.ExpireTimeSpan = TimeSpan.FromDays(14);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

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
            // Seed DB with initial data
            if(env.IsDevelopment())
            {
                using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                    if (!context.IsAllMigrationsApplied())
                    {
                        context.Database.Migrate();
                    }
                    context.EnsureSeedDevelopment();
                }
            }
            else
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.EnsureSeedProduction();
                }
            }

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
