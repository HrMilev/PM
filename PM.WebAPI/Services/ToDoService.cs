using AutoMapper;
using Microsoft.AspNetCore.Http;
using PM.Common.Models.Rest;
using PM.Data.Entities.ToDos;
using PM.Data.Repositories.Interfaces;
using PM.WebAPI.Services.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PM.WebAPI.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public ToDoService(IToDoRepository toDoRepository, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _toDoRepository = toDoRepository;
            _mapper = mapper;
            _httpContext = httpContext;
        }
        public async Task<ToDoRestModel> CreateAsync(ToDoRestModel toDoRestModel)
        {
            var todo = _mapper.Map<ToDo>(toDoRestModel);
            todo.UserId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            todo = await _toDoRepository.SaveAsync(todo);
            return _mapper.Map<ToDoRestModel>(todo);
        }
    }
}
