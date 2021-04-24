using Microsoft.EntityFrameworkCore;
using PM.Application.Interfaces.Repositories;
using PM.Application.Interfaces.Services;
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

        public ToDoService(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<int> CountAsync(string userId)
        {
            return await _toDoRepository.GetQueryable().Where(x => x.UserId == userId).CountAsync();
        }

        public async Task<ToDo> UpdateAsync(ToDo toDoRestModel, string userId)
        {
            var oldTodo = await _toDoRepository.GetAsync(toDoRestModel.Id);
            if (oldTodo == null || oldTodo.UserId != userId)
            {
                return null;
            }

            return await _toDoRepository.UpdateAsync(toDoRestModel);
        }

        public async Task<ToDo> CreateAsync(ToDo toDoRestModel, string userId)
        {
            toDoRestModel.UserId = userId;
            toDoRestModel.CreateDate = DateTime.UtcNow;
            return await _toDoRepository.SaveAsync(toDoRestModel);
        }

        public async Task<bool> DeleteAsync(string id, string userId)
        {
            return await _toDoRepository.DeleteAsync(x => x.UserId == userId && x.Id.ToString() == id);
        }

        public IList<ToDo> GetList(string userId)
        {
            var todos = _toDoRepository.GetList(t => t.UserId == userId);
            return todos;
        }

        public async Task<ToDo> GetAsync(string id, string userId)
        {
            var todo = await _toDoRepository.GetAsync(Guid.Parse(id));
            if (todo.UserId != userId)
            {
                return null;
            }
            return todo != null ? todo : null;
        }

        public async Task<IList<ToDo>> GetPageAsync(string userId, int page, int pageSize)
        {
            var todos = await _toDoRepository.GetQueryable()
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.StartDate)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();
            return todos;
        }
    }
}
