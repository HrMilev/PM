using AutoMapper;
using PM.Common.Models.Rest;
using PM.Data.Entities;

namespace PM.WebAPI.Automapper.Profiles
{
    public class FolderProfile : Profile
    {
        public FolderProfile()
        {
            CreateMap<Folder, FolderRestModel>();
            CreateMap<FolderRestModel, Folder>();
        }
    }
}
