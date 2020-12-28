using PM.Common.Models.Rest;
using PM.WebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Repositories.Interfaces
{
    public interface IUploadedFileRepository
    {
        Task<HttpResponseWrapper<string>> DeleteAsync(string id);
        Task<UploadedFileRestModel> GetFileAsync(string id);
        Task<IList<UploadedFileRestModel>> SaveListAsync(IList<UploadedFileRestModel> files, int folderId);
        Task<UploadedFileRestModel> UpdateAsync(UploadedFileRestModel file);
    }
}
