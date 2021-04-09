using PM.Application.Interfaces.Repositories.Base;
using PM.Domain;
using System.Threading.Tasks;

namespace PM.Application.Interfaces.Repositories
{
    public interface IFolderRepository : IRepositoryBase<Folder>
    {
        Task DeleteFolderAsync(int id, string userId);
    }
}