using AutoMapper;
using PM.Common.Models.Rest;
using PM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebAPI.Automapper.Profiles
{
    public class UploadedFileProfile : Profile
    {
        public UploadedFileProfile()
        {
            CreateMap<UploadedFile, UploadedFileRestModel>()
                .ForMember(x => x.Name, y => y.MapFrom(z => $"{z.Name}.{z.Extension}"));
            CreateMap<UploadedFileRestModel, UploadedFile>()
                .ForMember(x => x.Name, y => y.MapFrom(z => string.Join(string.Empty, z.Name.Split('.', StringSplitOptions.None).Take(z.Name.Split('.', StringSplitOptions.None).Count() - 1))))
                .ForMember(x => x.Extension, y => y.MapFrom(z => z.Name.Split('.', StringSplitOptions.None).Count() > 1 ? z.Name.Split('.', StringSplitOptions.None).TakeLast(1).FirstOrDefault() : string.Empty));
        }
    }
}
