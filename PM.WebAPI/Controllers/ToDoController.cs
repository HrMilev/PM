using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Common.Models.Rest;
using PM.WebAPI.Services.Interfaces;

namespace PM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        public IActionResult Get()
        {
            var todos = _toDoService.GetList();
            return Ok(todos);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ToDoRestModel toDoViewModel)
        {
            var todo = await _toDoService.CreateAsync(toDoViewModel);
            return StatusCode(201, todo);
        }
    }
}
