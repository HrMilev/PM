using PM.Data.Repositories.Bases.Interfaces;
using PM.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.Data.Repositories.Interfaces
{
    public interface IUploadedFileRepository : IRepositoryBase<UploadedFile>
    {
        Task<IList<UploadedFile>> SaveListAsync(IList<UploadedFile> files);
    }
}