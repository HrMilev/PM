using PM.Data.Repositories.Bases.Interfaces;
using PM.Domain;
using System.Threading.Tasks;

namespace PM.Data.Repositories.Interfaces
{
    public interface IFolderRepository : IRepositoryBase<Folder>
    {
        Task DeleteFolderAsync(int id, string userId);
    }
}