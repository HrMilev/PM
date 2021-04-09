using Microsoft.Extensions.DependencyInjection;
using PM.Application.Interfaces.Services;

namespace PM.Infrastructure.ServiceCollectionExtension
{
    public static class ServiceCollectionExtension
    {
        public static void AddPMInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IEmailSenderService, EmailSenderService>();
        }
    }
}
