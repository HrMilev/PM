﻿using PM.Common.Models.Rest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.WebAPI.Services.Interfaces
{
    public interface IToDoService
    {
        Task<ToDoRestModel> CreateAsync(ToDoRestModel toDoRestModel);
        IList<ToDoRestModel> GetList();
    }
}