using Microsoft.Extensions.DependencyInjection;
using PM.Common.Utils.Culture;
using PM.Data.Repositories;
using PM.Data.Repositories.Interfaces;
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
            services.AddTransient<IContactUsFormRepository, ContactUsFormRepository>();
            services.AddTransient<IContactUsFormService, ContactUsFormService>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddSingleton<ISupportedCulturesService, SupportedCulturesService>();
        }
    }
}
