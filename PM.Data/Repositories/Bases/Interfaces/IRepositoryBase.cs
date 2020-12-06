using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Data.Repositories.Bases.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task DeleteAsync(Func<T, bool> predicate);
        Task<IList<T>> GetAll(Func<T, bool> predicate);
        IQueryable<T> GetQueryable();
        Task<T> SaveAsync(T entity);
    }
}
