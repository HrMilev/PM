using PM.Common.Models.Rest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.WebAPI.Services.Interfaces
{
    public interface IToDoService : ICountableService
    {
        Task<ToDoRestModel> CreateAsync(ToDoRestModel toDoRestModel, string userId);
        Task<IList<ToDoRestModel>> GetList(string userId);
        Task<IList<ToDoRestModel>> GetPage(string userId, int page, int pageSize);
    }
}
