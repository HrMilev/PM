using PM.Common.Models.Rest;
using PM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Repositories.Interfaces
{
    public interface IToDoRepository
    {
        Task<ToDoRestModel> CreateAsync(ToDoRestModel toDo);
        Task<IEnumerable<ToDoRestModel>> GetToDosAsync();
        Task<(IEnumerable<ToDoRestModel>, int)> GetPageAsync(int page, int pageSize = 5);
        Task<HttpResponseWrapper<string>> DeleteAsync(string id);
        Task<ToDoRestModel> UpdateAsync(ToDoRestModel toDo);
    }
}
