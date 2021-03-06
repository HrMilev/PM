﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Application.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<bool> DeleteAsync(Func<T, bool> predicate);
        Task<T> GetAsync<Tid>(Tid id);
        IList<T> GetList(Func<T, bool> predicate);
        IQueryable<T> GetQueryable();
        Task<T> SaveAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}
