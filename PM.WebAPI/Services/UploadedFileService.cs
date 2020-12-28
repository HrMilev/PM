using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using PM.Common.Models.Rest;
using PM.Data.Entities;
using PM.Data.Repositories.Interfaces;
using PM.WebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebAPI.Services
{
    public class UploadedFileService : IUploadedFileService
    {
        private readonly IUploadedFileRepository _uploadedFileRepository;
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public UploadedFileService(IUploadedFileRepository uploadedFileRepository,
            IFolderRepository folderRepository,
            IMapper mapper,
            IWebHostEnvironment env)
        {
            _uploadedFileRepository = uploadedFileRepository;
            _folderRepository = folderRepository;
            _mapper = mapper;
            _env = env;
        }

        public async Task<UploadedFileRestModel> DownloadAsync(string userId, string id)
        {
            var file = await _uploadedFileRepository.GetAsync(Guid.Parse(id));
            if (file == null)
            {
                return null;
            }
            var path = $"{_env.WebRootPath}\\{userId}\\{id}";

            if (!File.Exists(path))
            {
                return null;
            }

            var restFile = _mapper.Map<UploadedFileRestModel>(file);
            restFile.Content = await File.ReadAllBytesAsync(path);
            return restFile;
        }

        public async Task<UploadedFileRestModel> UpdateAsync(string userId, UploadedFileRestModel file)
        {
            var oldFile = await _uploadedFileRepository.GetQueryable()
                .Include(x => x.Folder)
                .Where(x => x.Id.ToString() == file.Id)
                .FirstOrDefaultAsync();

            if (oldFile == null || oldFile.Folder.CreatorId != userId)
            {
                return null;
            }

            var newFile = _mapper.Map(file, oldFile);
            return _mapper.Map<UploadedFileRestModel>(await _uploadedFileRepository.UpdateAsync(newFile));
        }

        public async Task<IList<UploadedFileRestModel>> SaveFilesToFolder(string userId, int folderId, IList<UploadedFileRestModel> restFiles)
        {
            var folder = _folderRepository.GetList(x => x.Id == folderId);
            if (folder.Count() != 1 || folder.First().CreatorId != userId)
            {
                return null;
            }
            var filesToSave = new List<UploadedFile>();
            foreach (var restFile in restFiles)
            {
                var file = _mapper.Map<UploadedFile>(restFile);
                file.Id = Guid.NewGuid();
                file.FolderId = folderId;

                var pathToDirectory = $"{_env.WebRootPath}\\{userId}";
                Directory.CreateDirectory(pathToDirectory);
                var path = pathToDirectory + $"\\{file.Id}";
                using var fs = File.Create(path);
                fs.Write(restFile.Content, 0, restFile.Content.Length);

                filesToSave.Add(file);
            }

            var savedFiles = await _uploadedFileRepository.SaveListAsync(filesToSave);
            return _mapper.Map<IList<UploadedFileRestModel>>(savedFiles);
        }

        public async Task<bool> DeleteAsync(string userId, string id)
        {
            var path = $"{_env.WebRootPath}\\{userId}\\{id}";

            if (!File.Exists(path))
            {
                return false;
            }
            File.Delete(path);
            return await _uploadedFileRepository.DeleteAsync(x => x.Id.ToString() == id);
        }

        public async Task DeleteOrphanAsync(string userId)
        {
            var files = _uploadedFileRepository.GetList(x => x.FolderId == null);
            var deletedFilesId = new List<Guid>();
            foreach (var file in files)
            {
                var path = $"{_env.WebRootPath}\\{userId}\\{file.Id}";

                if (!File.Exists(path))
                {
                    continue;
                }

                File.Delete(path);
                deletedFilesId.Add(file.Id);
            }

            await _uploadedFileRepository.DeleteAsync(x => deletedFilesId.Contains(x.Id));
        }
    }
}
