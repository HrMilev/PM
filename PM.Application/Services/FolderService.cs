using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PM.Application.Interfaces.Repositories;
using PM.Application.Interfaces.Services;
using PM.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Application.Services
{
    public class FolderService : IFolderService
    {
        private readonly IFolderRepository _folderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUploadedFileService _uploadedFileService;

        public FolderService(IFolderRepository folderRepository,
            UserManager<ApplicationUser> userManager,
            IUploadedFileService uploadedFileService)
        {
            _folderRepository = folderRepository;
            _userManager = userManager;
            _uploadedFileService = uploadedFileService;
        }

        public async Task<Folder> GetTreeAsync(string userId)
        {
            var folders = _folderRepository.GetQueryable()
                .Include(x => x.ChildFolders)
                .Include(x => x.Files)
                .Where(x => x.CreatorId == userId)
                .ToList();

            var rootFolder = await GetTreeRoot(userId, folders);

            return rootFolder;
        }

        public async Task<Folder> CreateFolderAsync(string userId, Folder folderRest)
        {
            if (folderRest.ParentFolderId == null)
            {
                return null;
            }

            var fatherFolder = _folderRepository
                    .GetList(x => x.CreatorId == userId && x.Id == folderRest.ParentFolderId);

            if (fatherFolder.Count != 1)
            {
                return null;
            }

            folderRest.CreatorId = userId;
            return await _folderRepository.SaveAsync(folderRest);
        }

        public async Task<Folder> UpdateAsync(string userId, Folder folder)
        {
            var oldFolder = _folderRepository
                .GetList(x => x.CreatorId == userId && x.Id == folder.Id).FirstOrDefault();

            if (oldFolder == null || oldFolder.ParentFolderId == null)
            {
                return null;
            }

            return await _folderRepository.UpdateAsync(folder);
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var folder = _folderRepository
                .GetList(x => x.CreatorId == userId && x.Id == id).FirstOrDefault();

            if (folder == null || folder.ParentFolderId == null)
            {
                return;
            }

            await _folderRepository.DeleteFolderAsync(id, userId);
            await _uploadedFileService.DeleteOrphanAsync(userId);
        }

        private async Task<Folder> GetTreeRoot(string userId, List<Folder> folders)
        {
            if (folders.Count() == 0)
            {
                var currentUser = await _userManager.FindByIdAsync(userId);
                var rootFolder = new Folder { Name = "root", CreatorId = userId };
                currentUser.RootFolder = rootFolder;
                rootFolder = await _folderRepository.SaveAsync(rootFolder);
                folders.Add(rootFolder);
            }

            return folders.FirstOrDefault(x => x.ParentFolderId == null);
        }
    }
}
