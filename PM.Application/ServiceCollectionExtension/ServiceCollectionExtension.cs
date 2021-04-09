using Microsoft.Extensions.DependencyInjection;
using PM.Application.Interfaces.Services;
using PM.Application.Services;

namespace PM.Application.ServiceCollectionExtension
{
    public static class ServiceCollectionExtension
    {
        public static void AddPMApplication(this IServiceCollection services)
        {
            services.AddTransient<IToDoService, ToDoService>();
            services.AddTransient<IUserQuestionService, UserQuestionService>();
            services.AddTransient<IFolderService, FolderService>();
            services.AddTransient<IUploadedFileService, UploadedFileService>();
            services.AddTransient<IFileService, FileService>();
        }
    }
}
