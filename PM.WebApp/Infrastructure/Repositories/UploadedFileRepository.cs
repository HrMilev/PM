﻿using PM.Common.Models.Rest;
using PM.WebApp.Infrastructure.Repositories.Interfaces;
using PM.WebApp.Infrastructure.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Repositories
{
    public class UploadedFileRepository : IUploadedFileRepository
    {
        private const string URL = "api/file";
        private readonly IHttpService _httpService;

        public UploadedFileRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public string GetHref(string id)
        {
            return $"{URL}/{id}";
        }

        public async Task<UploadedFileRestModel> UpdateAsync(UploadedFileRestModel file)
        {
            var uri = $"{URL}/{file.Id}";
            var response = await _httpService.UpdateAsync(uri, file);

            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyAsync());
            }

            return response.Response;
        }

        public async Task<IList<UploadedFileRestModel>> SaveListAsync(IList<UploadedFileRestModel> files, int folderId)
        {
            string url = URL + $"/folder/{folderId}";
            var response = await _httpService.PostAsync<IList<UploadedFileRestModel>, IList<UploadedFileRestModel>>(url, files);

            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyAsync());
            }

            return response.Response;
        }
    }
}
