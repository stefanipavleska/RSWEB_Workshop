using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BooksStore.Data;
using BooksStore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using BooksStore.Areas.Identity.Data;
using BooksStore.Interfaces;
using BooksStore.Services;

namespace Boook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<BooksStoreContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BooksStoreContext") ?? throw new InvalidOperationException("Connection string 'BooksStoreContext' not found.")));


            //  builder.Services.AddRazorPages();   
            // builder.Services.AddDefaultIdentity<WorkshopImprovedUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<WorkshopImprovedContext>();

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            builder.Services.AddRazorPages();

            builder.Services.AddIdentity<BooksStoreUser, IdentityRole>().AddEntityFrameworkStores<BooksStoreContext>().AddDefaultUI().AddDefaultTokenProviders();


            builder.Services.AddHttpContextAccessor();


            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
            });

            //Password Strength Setting
            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;
                // User settings
                options.User.RequireUniqueEmail = true;
            });
            //Setting the Account Login page
            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });


            //builder.Services.AddTransient<IBufferedFileUploadService, BufferedFileUploadLocalService>(); */

            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IBufferedFileUploadService, BufferedFileUploadLocalService>();
            builder.Services.AddTransient<IBufferedFileUploadService1, BufferedFileUploadLocalService1>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedData.Initialize(services);
                // SeedData.CreateRolesAndAdminUser(services);
            }



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCors();
            app.UseRouting();
            app.UseAuthentication(); ;

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Books}/{action=Index}/{id?}");

            app.MapRazorPages();
            // app.UseAuthentication();;



            app.Run();
        }
    }
}