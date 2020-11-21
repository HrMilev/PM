using System.Collections.Generic;
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

        public ActionResult<IEnumerable<ToDoRestModel>> Get([FromQuery] PageableRestModel pageableRestModel)
        {
            var todos = _toDoService.GetList();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public ActionResult<ToDoRestModel> Get(int id)
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ToDoRestModel toDoViewModel)
        {
            var todo = await _toDoService.CreateAsync(toDoViewModel);
            return CreatedAtAction("GET", todo.Id, todo);
        }
    }
}
