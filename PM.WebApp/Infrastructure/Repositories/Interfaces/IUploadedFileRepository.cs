using PM.Common.Models.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Repositories.Interfaces
{
    public interface IUploadedFileRepository
    {
        string GetHref(string id);
        Task<IList<UploadedFileRestModel>> SaveListAsync(IList<UploadedFileRestModel> files, int folderId);
        Task<UploadedFileRestModel> UpdateAsync(UploadedFileRestModel file);
    }
}
