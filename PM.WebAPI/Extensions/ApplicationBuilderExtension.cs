using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PM.Common.Strings;
using PM.Data.Entities;

namespace PM.WebAPI.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void EnsureMigrationOf<T>(this IApplicationBuilder app) where T : DbContext
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<T>();
            context.Database.Migrate();
        }

        public static void SeedAdmin(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!roleManager.RoleExistsAsync(UserRole.Admin).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = UserRole.Admin;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var config = serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();
            if (userManager.FindByEmailAsync(config["ApplicationUsers:Admin:Email"]).Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = config["ApplicationUsers:Admin:UserName"],
                    Email = config["ApplicationUsers:Admin:Email"]
                };
                var result = userManager.CreateAsync(user, config["ApplicationUsers:Admin:Password"]).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, UserRole.Admin).Wait();
                }
            }
        }
    }
}
