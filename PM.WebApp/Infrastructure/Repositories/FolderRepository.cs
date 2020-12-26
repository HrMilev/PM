using PM.Common.Models.Rest;
using PM.WebApp.Infrastructure.Repositories.Interfaces;
using PM.WebApp.Infrastructure.Utils.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        private const string URL = "api/folderTree";
        private readonly IHttpService _httpService;

        public FolderRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<FolderRestModel> GetTreeAsync()
        {
            var response = await _httpService.GetAsync<FolderRestModel>(URL);

            if (!response.IsSuccess && response.ResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyAsync());
            }

            return response.Response;
        }

        public async Task<FolderRestModel> CreateFolderAsync(FolderRestModel folder)
        {
            var response = await _httpService.PostAsync<FolderRestModel, FolderRestModel>(URL, folder);

            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyAsync());
            }

            return response.Response;
        }

        public async Task<FolderRestModel> UpdateAsync(FolderRestModel folder)
        {
            var uri = $"{URL}/{folder.Id}";
            var response = await _httpService.UpdateAsync(uri, folder);

            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyAsync());
            }

            return response.Response;
        }

        public async Task<string> DeleteAsync(int id)
        {
            string url = URL + $"/{id}";
            var response = await _httpService.DeleteAsync<string>(url);
            return response.Response;
        }
    }
}
