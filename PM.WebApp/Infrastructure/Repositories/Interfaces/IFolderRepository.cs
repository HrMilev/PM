using PM.Common.Models.Rest;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Repositories.Interfaces
{
    public interface IFolderRepository
    {
        Task<FolderRestModel> CreateFolderAsync(FolderRestModel folder);
        Task<FolderRestModel> GetTreeAsync();
        Task<FolderRestModel> UpdateAsync(FolderRestModel folder);
    }
}