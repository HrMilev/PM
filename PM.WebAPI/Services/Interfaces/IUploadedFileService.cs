﻿using PM.Common.Models.Rest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.WebAPI.Services.Interfaces
{
    public interface IUploadedFileService
    {
        Task<UploadedFileRestModel> UpdateAsync(string userId, UploadedFileRestModel file);
        Task<IList<UploadedFileRestModel>> SaveFilesToFolder(string userId, int folderId, IList<UploadedFileRestModel> restFiles);
        Task<UploadedFileRestModel> DownloadAsync(string userId, string id);
    }
}