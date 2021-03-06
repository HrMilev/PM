﻿using PM.Application.Interfaces.Repositories.Base;
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

        public virtual async Task<T> UpdateAsync(T entity)
        {
            this.ThrowIfNull(entity, nameof(this.UpdateAsync));

            try
            {
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{typeof(T).Name} {nameof(this.UpdateAsync)} failed: {ex.InnerException}");
            }
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

        public virtual async Task<T> GetAsync<Tid>(Tid id)
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public virtual IList<T> GetList(Func<T, bool> predicate)
        {
            var queryable = _dbContext.Set<T>().AsQueryable().Where(predicate);
            return queryable.ToList();
        }

        public virtual async Task<bool> DeleteAsync(Func<T, bool> predicate)
        {
            var entitiesToRemove = _dbContext.Set<T>().Where(predicate);
            _dbContext.Set<T>().RemoveRange(entitiesToRemove);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        protected void ThrowIfNull<Type>(Type entity, string methodName)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{methodName} {typeof(T).Name} entity cannot be null");
            }
        }
    }
}
