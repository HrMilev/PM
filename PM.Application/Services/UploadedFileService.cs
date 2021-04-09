using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PM.Application.Interfaces.Repositories;
using PM.Application.Interfaces.Services;
using PM.Common.Models.Rest;
using PM.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Application.Services
{
    public class UploadedFileService : IUploadedFileService
    {
        private readonly IUploadedFileRepository _uploadedFileRepository;
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public UploadedFileService(
            IUploadedFileRepository uploadedFileRepository,
            IFolderRepository folderRepository,
            IMapper mapper,
            IFileService fileService)
        {
            _uploadedFileRepository = uploadedFileRepository;
            _folderRepository = folderRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<UploadedFileRestModel> DownloadAsync(string userId, string id)
        {
            var file = await _uploadedFileRepository.GetAsync(Guid.Parse(id));
            if (file == null)
            {
                return null;
            }

            var path = _fileService.GetUserFilePathIfExists(userId, id);

            if (path == null)
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

                await _fileService.WriteUserFileToFileSystem(userId, file.Id.ToString(), restFile.Content);

                filesToSave.Add(file);
            }

            var savedFiles = await _uploadedFileRepository.SaveListAsync(filesToSave);
            return _mapper.Map<IList<UploadedFileRestModel>>(savedFiles);
        }

        public async Task<bool> DeleteAsync(string userId, string id)
        {
            if (!_fileService.DeleteUserFile(userId, id))
            {
                return false;
            }

            return await _uploadedFileRepository.DeleteAsync(x => x.Id.ToString() == id);
        }

        public async Task DeleteOrphanAsync(string userId)
        {
            var files = _uploadedFileRepository.GetList(x => x.FolderId == null);
            var deletedFilesId = new List<Guid>();
            foreach (var file in files)
            {
                if (!_fileService.DeleteUserFile(userId, file.Id.ToString()))
                {
                    continue;
                }

                deletedFilesId.Add(file.Id);
            }

            await _uploadedFileRepository.DeleteAsync(x => deletedFilesId.Contains(x.Id));
        }
    }
}
