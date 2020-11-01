using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.Data.Repositories.Bases.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IList<T>> GetAllAsync();
        Task<T> SaveAsync(T entity);
    }
}
