using PM.Data.Entities;
using PM.Data.Repositories.Bases;
using PM.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.Data.Repositories
{
    public class UploadedFileRepository : RepositoryBase<UploadedFile>, IUploadedFileRepository
    {
        public UploadedFileRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<UploadedFile>> SaveListAsync(IList<UploadedFile> files)
        {
            this.ThrowIfNull(files, nameof(this.SaveListAsync));

            try
            {
                _dbContext.UploadedFiles.AddRange(files);
                await _dbContext.SaveChangesAsync();
                return files;
            }
            catch (Exception ex)
            {
                throw new Exception($"{typeof(IList<UploadedFile>).Name} {nameof(this.SaveListAsync)} failed: {ex.InnerException}");
            }
        }
    }
}
