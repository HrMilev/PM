using Microsoft.Extensions.DependencyInjection;
using PM.Common.Utils.Culture;
using PM.WebApp.Infrastructure.Repositories;
using PM.WebApp.Infrastructure.Repositories.Interfaces;
using PM.WebApp.Infrastructure.Utils;
using PM.WebApp.Infrastructure.Utils.Interfaces;

namespace PM.WebApp.Configurations
{
    public static class ServiceCollectionExtension
    {
        public static void AddPMServices(this IServiceCollection services)
        {
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<IToDoRepository, ToDoRepository>();
            services.AddTransient<IUserQuestionRepository, UserQuestionRepository>();
            services.AddTransient<IFolderRepository, FolderRepository>();
            services.AddSingleton<ISupportedCulturesService, SupportedCulturesService>();
            services.AddSingleton<IAlertService, AlertService>();
            services.AddSingleton<IEventHandlerService, EventHandlerService>();
        }
    }
}
