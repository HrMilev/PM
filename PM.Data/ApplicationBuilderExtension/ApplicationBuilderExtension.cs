using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PM.Common.Strings;
using PM.Domain;

namespace PM.Data.ApplicationBuilderExtension
{
    public static class ApplicationBuilderExtension
    {
        public static void EnsureMigrationOf<T>(this IApplicationBuilder app) where T : DbContext
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<T>();
            context.Database.Migrate();
            SeedAdmin(app);
        }

        private static void SeedAdmin(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!roleManager.RoleExistsAsync(UserRole.Admin).Result)
            {
                var role = new IdentityRole();
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
