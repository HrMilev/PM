using PM.Domain;
using System.Threading.Tasks;

namespace PM.Application.Interfaces.Services
{
    public interface IFolderService
    {
        Task<Folder> CreateFolderAsync(string userId, Folder folderRest);
        Task DeleteAsync(int id, string userId);
        Task<Folder> GetTreeAsync(string userId);
        Task<Folder> UpdateAsync(string userId, Folder folder);
    }
}