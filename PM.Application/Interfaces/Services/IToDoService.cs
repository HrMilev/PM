using PM.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.Application.Interfaces.Services
{
    public interface IToDoService : ICountableService
    {
        Task<ToDo> CreateAsync(ToDo toDoRestModel, string userId);
        IList<ToDo> GetList(string userId);
        Task<IList<ToDo>> GetPageAsync(string userId, int page, int pageSize);
        Task<bool> DeleteAsync(string id, string userId);
        Task<ToDo> GetAsync(string id, string userId);
        Task<ToDo> UpdateAsync(ToDo toDoRestModel, string userId);
    }
}
