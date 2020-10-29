using AutoMapper;
using PM.Common.Models.Rest;
using PM.WebAPI.Models.Entities.ToDoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebAPI.Automapper.Profiles
{
    public class ToDoProfile : Profile
    {
        public ToDoProfile()
        {
            CreateMap<ToDo, ToDoRestModel>().ReverseMap();
        }
    }
}
