using Microsoft.EntityFrameworkCore;
using PM.Application.Interfaces.Repositories;
using PM.Application.Interfaces.Services;
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
        private readonly IFileService _fileService;

        public UploadedFileService(
            IUploadedFileRepository uploadedFileRepository,
            IFolderRepository folderRepository,
            IFileService fileService)
        {
            _uploadedFileRepository = uploadedFileRepository;
            _folderRepository = folderRepository;
            _fileService = fileService;
        }

        public async Task<(UploadedFile File, byte[] Content)> DownloadAsync(string userId, string id)
        {
            var file = await _uploadedFileRepository.GetAsync(Guid.Parse(id));
            if (file == null)
            {
                return (null, null);
            }

            var path = _fileService.GetUserFilePathIfExists(userId, id);

            if (path == null)
            {
                return (null, null);
            }

            return (file, await File.ReadAllBytesAsync(path));
        }

        public async Task<UploadedFile> UpdateAsync(string userId, UploadedFile file)
        {
            var oldFile = await _uploadedFileRepository.GetQueryable()
                .Include(x => x.Folder)
                .Where(x => x.Id == file.Id)
                .FirstOrDefaultAsync();

            if (oldFile == null || oldFile.Folder.CreatorId != userId)
            {
                return null;
            }

            return await _uploadedFileRepository.UpdateAsync(file);
        }

        public async Task<IList<UploadedFile>> SaveFilesToFolder(string userId, int folderId, IList<(UploadedFile File, byte[] Content)> restFiles)
        {
            var folder = _folderRepository.GetList(x => x.Id == folderId);
            if (folder.Count() != 1 || folder.First().CreatorId != userId)
            {
                return null;
            }
            var filesToSave = new List<UploadedFile>();
            foreach (var restFile in restFiles)
            {
                restFile.File.Id = Guid.NewGuid();
                restFile.File.FolderId = folderId;

                await _fileService.WriteUserFileToFileSystem(userId, restFile.File.Id.ToString(), restFile.Content);

                filesToSave.Add(restFile.File);
            }

            var savedFiles = await _uploadedFileRepository.SaveListAsync(filesToSave);
            return savedFiles;
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
