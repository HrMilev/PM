using PM.Common.Models.Rest;
using System.Threading.Tasks;

namespace PM.WebAPI.Services.Interfaces
{
    public interface IToDoService
    {
        Task<ToDoRestModel> CreateAsync(ToDoRestModel toDoRestModel);
    }
}
