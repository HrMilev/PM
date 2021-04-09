using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PM.Application.Interfaces.Repositories;
using PM.Application.Interfaces.Services;
using PM.Common.Models.Rest;
using PM.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Application.Services
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

        public async Task<ToDoRestModel> UpdateAsync(ToDoRestModel toDoRestModel, string userId)
        {
            var oldTodo = await _toDoRepository.GetAsync(Guid.Parse(toDoRestModel.Id));
            if (oldTodo == null || oldTodo.UserId != userId)
            {
                return null;
            }

            var todo = _mapper.Map(toDoRestModel, oldTodo);
            var updatedTodo = await _toDoRepository.UpdateAsync(todo);
            return _mapper.Map<ToDoRestModel>(updatedTodo);
        }

        public async Task<ToDoRestModel> CreateAsync(ToDoRestModel toDoRestModel, string userId)
        {
            var todo = _mapper.Map<ToDo>(toDoRestModel);
            todo.UserId = userId;
            todo.CreateDate = DateTime.UtcNow;
            todo = await _toDoRepository.SaveAsync(todo);
            return _mapper.Map<ToDoRestModel>(todo);
        }

        public async Task<bool> DeleteAsync(string id, string userId)
        {
            return await _toDoRepository.DeleteAsync(x => x.UserId == userId && x.Id.ToString() == id);
        }

        public IList<ToDoRestModel> GetList(string userId)
        {
            var todos = _toDoRepository.GetList(t => t.UserId == userId);
            return _mapper.Map<IList<ToDoRestModel>>(todos);
        }

        public async Task<ToDoRestModel> GetAsync(string id, string userId)
        {
            var todo = await _toDoRepository.GetAsync(Guid.Parse(id));
            if (todo.UserId != userId)
            {
                return null;
            }
            return todo != null ? _mapper.Map<ToDoRestModel>(todo) : null;
        }

        public async Task<IList<ToDoRestModel>> GetPageAsync(string userId, int page, int pageSize)
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
