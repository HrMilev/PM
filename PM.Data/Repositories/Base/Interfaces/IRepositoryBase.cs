using System.Threading.Tasks;

namespace PM.WebAPI.Data.Repositories.Base.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> SaveAsync(T entity);
    }
}
