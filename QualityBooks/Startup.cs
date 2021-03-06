﻿using System;
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
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.HttpOnly = true;

            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, ApplicationDbContext db)

        {
           // db.Database.Migrate();
            app.UseSession();
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
                routes.MapAreaRoute("shoppingcart", "shoppingcart", "{controller=Order}/{action=Index}/{id?}");
            });

            await CreateRoles(serviceProvider);
           

        }
    

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                // Create database schema if none exists
                var apContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                apContext.Database.EnsureCreated();

                //if there is already an administrator role abort.
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManger = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                string[] roleNames = {"Admin", "Member"};

                foreach (var roleName in roleNames)
                {
                    bool roleExist = roleManager.RoleExistsAsync(roleName).Result;
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                await AddUser(serviceScope, userManger, "admin", Configuration.GetSection("AdminUserSettings")["UserEmail"], "Admin Address", Configuration.GetSection("AdminUserSettings")["UserPassword"], "Admin");
                await AddUser(serviceScope, userManger, "user", Configuration.GetSection("userSettings")["UserEmail"], "Customer Address", Configuration.GetSection("UserSettings")["UserPassword"], "Member");
            }
        }

        private async Task AddUser(IServiceScope serviceScope, UserManager<ApplicationUser> userManger, string name, string userName, string address, string password, string role)
        {
            var powerUser = new ApplicationUser
            {
                Name = name,
                UserName = userName,
                Email = userName,
                EmailConfirmed = true,
                Address = address,
                HomePhone = "123-1234",
                LockoutEnabled = false,
                Enabled = true
            };
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
            var test = userManger.FindByEmailAsync(userName);
            if (test.Result == null)
            {
                string userPassword = password;
                powerUser.EmailConfirmed = true;
                var createPowerUser = await userManager.CreateAsync(powerUser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(powerUser, role);
                }
               
            }
        }
    }
}
    

