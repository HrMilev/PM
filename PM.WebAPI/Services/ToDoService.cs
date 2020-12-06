using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PM.Common.Models.Rest;
using PM.Data.Entities.ToDos;
using PM.Data.Repositories.Interfaces;
using PM.WebAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PM.WebAPI.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IMapper _mapper;

        public ToDoService(IToDoRepository toDoRepository, IMapper mapper)
        {
            _toDoRepository = toDoRepository;
            _mapper = mapper;
        }

        public async Task<int> CountAsync(string userId)
        {
            return await _toDoRepository.GetQueryable().Where(x => x.UserId == userId).CountAsync();
        }

        public async Task<ToDoRestModel> CreateAsync(ToDoRestModel toDoRestModel, string userId)
        {
            var todo = _mapper.Map<ToDo>(toDoRestModel);
            todo.UserId = userId;
            todo = await _toDoRepository.SaveAsync(todo);
            return _mapper.Map<ToDoRestModel>(todo);
        }

        public async Task<IList<ToDoRestModel>> GetList(string userId)
        {
            var todos = await _toDoRepository.GetAll(t => t.UserId == userId);
            return _mapper.Map<IList<ToDoRestModel>>(todos);
        }

        public async Task<IList<ToDoRestModel>> GetPage(string userId, int page, int pageSize)
        {
            var todos = await _toDoRepository.GetQueryable()
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.StartDate)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<IList<ToDoRestModel>>(todos);
        }
    }
}
