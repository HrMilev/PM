using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PM.Common.Models.Rest;
using PM.Data.Entities;
using PM.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebAPI.Services
{
    public class FolderService : IFolderService
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public FolderService(IFolderRepository folderRepository,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<FolderRestModel> GetTreeAsync(string userId)
        {
            var folders = _folderRepository.GetQueryable()
                .Include(x => x.ChildFolders)
                .Where(x => x.CreatorId == userId)
                .ToList();

            var rootFolder = await GetTreeRoot(userId, folders);

            return _mapper.Map<FolderRestModel>(rootFolder);
        }

        public async Task<FolderRestModel> CreateFolderAsync(string userId, FolderRestModel folderRest)
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

            var folder = _mapper.Map<Folder>(folderRest);
            folder.CreatorId = userId;
            folder = await _folderRepository.SaveAsync(folder);
            return _mapper.Map<FolderRestModel>(folder);
        }

        public async Task<FolderRestModel> RenameAsync(int id, string userId, string name)
        {
            var folder = _folderRepository
                .GetList(x => x.CreatorId == userId && x.Id == id).FirstOrDefault();

            if (folder == null || folder.ParentFolderId == null)
            {
                return null;
            }

            folder.Name = name;
            await _folderRepository.UpdateAsync(folder);
            return _mapper.Map<FolderRestModel>(folder);
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
