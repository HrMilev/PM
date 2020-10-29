using PM.Common.Models.Rest;
using PM.WebAPI.Models.Entities.ToDoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebAPI.Services.Interfaces
{
    public interface IToDoService
    {
        Task<ToDo> CreateAsync(ToDoRestModel toDoRestModel);
    }
}
