using AutoMapper;
using Microsoft.AspNetCore.Http;
using PM.Common.Models.Rest;
using PM.Data.Entities;
using System;
using System.Security.Claims;

namespace PM.WebAPI.Automapper.Profiles
{
    public class ToDoProfile : Profile
    {
        public ToDoProfile()
        {
            CreateMap<ToDo, ToDoRestModel>();
            CreateMap<ToDoRestModel, ToDo>()
                .ForMember(x => x.StartDate, y => y.MapFrom(z => z.StartDate.ToUniversalTime()))
                .ForMember(x => x.EndDate, y => y.MapFrom(z => z.EndDate.ToUniversalTime()));
        }
    }
}
