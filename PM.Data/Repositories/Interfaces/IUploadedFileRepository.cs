using PM.Data.Entities;
using PM.Data.Repositories.Bases.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.Data.Repositories.Interfaces
{
    public interface IUploadedFileRepository : IRepositoryBase<UploadedFile>
    {
        Task<IList<UploadedFile>> SaveListAsync(IList<UploadedFile> files);
    }
}