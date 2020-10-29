using PM.Common.Models.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Repositories.Interfaces
{
    public interface IToDoRepository
    {
        Task Create(ToDoRestModel toDo);
    }
}
