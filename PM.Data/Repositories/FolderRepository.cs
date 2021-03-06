﻿using Microsoft.EntityFrameworkCore;
using PM.Application.Interfaces.Repositories;
using PM.Data.Repositories.Bases;
using PM.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Data.Repositories
{
    public class FolderRepository : RepositoryBase<Folder>, IFolderRepository
    {
        public FolderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task DeleteFolderAsync(int id, string userId)
        {
            await _dbContext.Database.ExecuteSqlInterpolatedAsync($"DeleteFolder {id}, {userId}");
        }
    }
}
