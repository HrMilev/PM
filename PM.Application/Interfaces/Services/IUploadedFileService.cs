using PM.Common.Models.Rest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.Application.Interfaces.Services
{
    public interface IUploadedFileService
    {
        Task<UploadedFileRestModel> UpdateAsync(string userId, UploadedFileRestModel file);
        Task<IList<UploadedFileRestModel>> SaveFilesToFolder(string userId, int folderId, IList<UploadedFileRestModel> restFiles);
        Task<UploadedFileRestModel> DownloadAsync(string userId, string id);
        Task<bool> DeleteAsync(string userId, string id);
        Task DeleteOrphanAsync(string userId);
    }
}