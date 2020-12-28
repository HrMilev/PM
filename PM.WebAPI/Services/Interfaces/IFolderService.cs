using PM.Common.Models.Rest;
using System.Threading.Tasks;

namespace PM.WebAPI.Services
{
    public interface IFolderService
    {
        Task<FolderRestModel> CreateFolderAsync(string userId, FolderRestModel folderRest);
        Task DeleteAsync(int id, string userId);
        Task<FolderRestModel> GetTreeAsync(string userId);
        Task<FolderRestModel> UpdateAsync(string userId, FolderRestModel folder);
    }
}