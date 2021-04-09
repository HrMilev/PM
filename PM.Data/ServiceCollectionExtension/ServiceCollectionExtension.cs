using Microsoft.Extensions.DependencyInjection;
using PM.Application.Interfaces.Repositories;
using PM.Data.Repositories;

namespace PM.Data.ServiceCollectionExtension
{
    public static class ServiceCollectionExtension
    {
        public static void AddPMData(this IServiceCollection services)
        {
            services.AddTransient<IToDoRepository, ToDoRepository>();
            services.AddTransient<IUserQuestionRepository, UserQuestionRepository>();
            services.AddTransient<IFolderRepository, FolderRepository>();
            services.AddTransient<IUploadedFileRepository, UploadedFileRepository>();
        }
    }
}
