using AutoMapper;
using PM.Common.Models.Rest;
using PM.Domain;

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
