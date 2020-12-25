using PM.Common.Models.Rest;
using System.Threading.Tasks;

namespace PM.WebAPI.Services
{
    public interface IFolderService
    {
        Task<FolderRestModel> CreateFolderAsync(string userId, FolderRestModel folderRest);
        Task DeleteAsync(int id, string userId);
        FolderRestModel GetTree(string userId);
        Task<FolderRestModel> RenameAsync(int id, string userId, string name);
    }
}