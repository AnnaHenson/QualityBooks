using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QualityBooks.Data;
using QualityBooks.Data.Migrations;
using QualityBooks.Models;
using QualityBooks.Services;

namespace QualityBooks
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider,
            UserManager<ApplicationUser> userManager)
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

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapAreaRoute("catalogue", "catalogue", "{controller=Book}/{action=Index}/{id?}");
            });

            await CreateRoles(serviceProvider);
            {
                
            }

        }
    

        

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                // Create database schema if none exists
                var apContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                apContext.Database.EnsureCreated();

                //if there is already an administrator role abort.
                var _roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var _userManger = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                string[] roleNames = {"Admin", "Member"};
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    bool roleExist = _roleManager.RoleExistsAsync(roleName).Result;
                    if (!roleExist)
                    {
                        roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                var poweruser = new ApplicationUser
                {
                    UserName = Configuration.GetSection("userSettings")["UserEmail"],
                    Email = Configuration.GetSection("UserSettings")["userEmail"],
                    EmailConfirmed = true,
                    Enabled = true,
                    Address = "Addmin Address",
                    

                };
                var _userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var test = _userManger.FindByEmailAsync(Configuration.GetSection("userSettings")["UserEmail"]);
                if (test.Result == null)
                {
                    string userPassword = Configuration.GetSection("UserSettings")["UserPassword"];
                    poweruser.EmailConfirmed = true;
                    var createPowerUser = await _userManager.CreateAsync(poweruser, userPassword);
                    if (createPowerUser.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(poweruser, "Admin");
                        {


                        }


                        throw

                            new NotImplementedException();
                    }
                }
            }
        }
    }
}
    

