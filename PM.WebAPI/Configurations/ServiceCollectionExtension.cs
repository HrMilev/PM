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
            services.AddTransient<IUserQuestionRepository, UserQuestionRepository>();
            services.AddTransient<IUserQuestionService, UserQuestionService>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddSingleton<ISupportedCulturesService, SupportedCulturesService>();
            services.AddTransient<IFolderRepository, FolderRepository>();
            services.AddTransient<IFolderService, FolderService>();
            services.AddTransient<IUploadedFileRepository, UploadedFileRepository>();
            services.AddTransient<IUploadedFileService, UploadedFileService>();
            services.AddTransient<IFileService, FileService>();
        }
    }
}
