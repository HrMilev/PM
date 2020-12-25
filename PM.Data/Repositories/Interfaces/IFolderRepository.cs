using PM.Data.Entities;
using PM.Data.Repositories.Bases.Interfaces;
using System.Threading.Tasks;

namespace PM.Data.Repositories.Interfaces
{
    public interface IFolderRepository : IRepositoryBase<Folder>
    {
        Task DeleteFolderAsync(int id, string userId);
    }
}