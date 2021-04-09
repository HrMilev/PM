using Microsoft.Extensions.DependencyInjection;
using PM.Common.Utils.Culture;

namespace PM.WebAPI.Configurations
{
    public static class ServiceCollectionExtension
    {
        public static void AddPMServices(this IServiceCollection services)
        {
            services.AddSingleton<ISupportedCulturesService, SupportedCulturesService>();
        }
    }
}
