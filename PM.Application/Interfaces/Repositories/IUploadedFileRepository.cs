using PM.Application.Interfaces.Repositories.Base;
using PM.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.Application.Interfaces.Repositories
{
    public interface IUploadedFileRepository : IRepositoryBase<UploadedFile>
    {
        Task<IList<UploadedFile>> SaveListAsync(IList<UploadedFile> files);
    }
}