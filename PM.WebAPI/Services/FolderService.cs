using AutoMapper;
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

        public FolderService(IFolderRepository folderRepository,
            IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
        }

        public FolderRestModel GetTree(string userId)
        {
            var folders = _folderRepository.GetQueryable()
                .Include(x => x.ChildFolders)
                .Where(x => x.CreatorId == userId)
                .ToList();

            var rootFolder = CreateTree(folders, userId);
            return _mapper.Map<FolderRestModel>(rootFolder);
        }

        public async Task<FolderRestModel> CreateFolderAsync(string userId, FolderRestModel folderRest)
        {
            if (folderRest.ParentFolderId != null)
            {
                var fatherFolder = _folderRepository
                        .GetList(x => x.CreatorId == userId && x.Id == folderRest.ParentFolderId);

                if (fatherFolder.Count != 1)
                {
                    return null;
                }
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

            if (folder == null)
            {
                return null;
            }

            folder.Name = name;
            await _folderRepository.UpdateAsync(folder);
            return _mapper.Map<FolderRestModel>(folder);
        }

        public async Task DeleteAsync(int id, string userId)
        {
            await _folderRepository.DeleteFolderAsync(id, userId);
        }

        private Folder CreateTree(IList<Folder> folders, string userId)
        {
            var rootFolder = new Folder { Name = "", CreatorId = userId };
            rootFolder.ChildFolders = folders.Where(x => x.ParentFolderId == null).ToList();
            return rootFolder;
        }
    }
}
