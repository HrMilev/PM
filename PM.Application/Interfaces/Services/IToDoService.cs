using PM.Common.Models.Rest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.Application.Interfaces.Services
{
    public interface IToDoService : ICountableService
    {
        Task<ToDoRestModel> CreateAsync(ToDoRestModel toDoRestModel, string userId);
        IList<ToDoRestModel> GetList(string userId);
        Task<IList<ToDoRestModel>> GetPageAsync(string userId, int page, int pageSize);
        Task<bool> DeleteAsync(string id, string userId);
        Task<ToDoRestModel> GetAsync(string id, string userId);
        Task<ToDoRestModel> UpdateAsync(ToDoRestModel toDoRestModel, string userId);
    }
}
