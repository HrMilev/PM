using Microsoft.EntityFrameworkCore;
using PM.Data.Repositories.Bases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Data.Repositories.Bases
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public virtual async Task<T> SaveAsync(T entity)
        {
            this.ThrowIfNull(entity, nameof(this.SaveAsync));

            try
            {
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{typeof(T).Name} {nameof(this.SaveAsync)} failed: {ex.InnerException}");
            }
        }

        public virtual async Task<IList<T>> GetAll(Func<T, bool> predicate)
        {
            var a = _dbContext.Set<T>().Where(predicate).AsQueryable();
            return await a.ToListAsync();
        }

        public virtual async Task DeleteAsync(Func<T, bool> predicate)
        {
            var entitiesToRemove = _dbContext.Set<T>().Where(predicate);
            _dbContext.Set<T>().RemoveRange(entitiesToRemove);
            await _dbContext.SaveChangesAsync();
        }

        private void ThrowIfNull(T entity, string methodName)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{methodName} {typeof(T).Name} entity cannot be null");
            }
        }
    }
}
