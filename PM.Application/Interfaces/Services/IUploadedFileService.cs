using PM.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.Application.Interfaces.Services
{
    public interface IUploadedFileService
    {
        Task<UploadedFile> UpdateAsync(string userId, UploadedFile file);
        Task<IList<UploadedFile>> SaveFilesToFolder(string userId, int folderId, IList<(UploadedFile File, byte[] Content)> restFiles);
        Task<(UploadedFile File, byte[] Content)> DownloadAsync(string userId, string id);
        Task<bool> DeleteAsync(string userId, string id);
        Task DeleteOrphanAsync(string userId);
    }
}