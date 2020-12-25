using Microsoft.EntityFrameworkCore;
using PM.Data.Entities;
using PM.Data.Repositories.Bases;
using PM.Data.Repositories.Interfaces;
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
