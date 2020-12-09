using PM.Common.Models.Rest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.WebAPI.Services.Interfaces
{
    public interface IToDoService : ICountableService
    {
        Task<ToDoRestModel> CreateAsync(ToDoRestModel toDoRestModel, string userId);
        Task<IList<ToDoRestModel>> GetListAsync(string userId);
        Task<IList<ToDoRestModel>> GetPageAsync(string userId, int page, int pageSize);
        Task DeleteAsync(string id, string userId);
        Task<ToDoRestModel> GetAsync(string id, string userId);
        Task<ToDoRestModel> UpdateAsync(ToDoRestModel toDoRestModel, string userId);
    }
}
