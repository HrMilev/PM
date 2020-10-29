using PM.WebAPI.Data.Repositories.Base.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PM.WebAPI.Data.Repositories.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> SaveAsync(T entity)
        {
            if (entity == null)
            {
                var typeName = this.GetType().Name;
                var st = new StackTrace();
                var sf = st.GetFrame(1);

                var mn = sf.GetMethod().Name;
                throw new ArgumentNullException($"{typeName} {mn} entity can not be null");
            }

            try
            {
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.InnerException}"); ;
            }
        }
    }
}
