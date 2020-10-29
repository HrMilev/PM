using Microsoft.Extensions.DependencyInjection;
using PM.WebAPI.Data.Repositories;
using PM.WebAPI.Data.Repositories.Interfaces;
using PM.WebAPI.Services;
using PM.WebAPI.Services.Interfaces;

namespace PM.WebAPI.Configurations
{
    public static class ServiceCollectionExtension
    {
        public static void AddPMServices(this IServiceCollection services)
        {
            services.AddTransient<IToDoRepository, ToDoRepository>();
            services.AddTransient<IToDoService, ToDoService>();
        }
    }
}
