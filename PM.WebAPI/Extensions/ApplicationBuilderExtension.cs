using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
