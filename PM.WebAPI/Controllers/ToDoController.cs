using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Common.Models.Rest;
using PM.WebAPI.Extensions;
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

        public async Task<ActionResult<IEnumerable<ToDoRestModel>>> Get([FromQuery] int page)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var pageSize = await Request.GetPageSizePagination(Response, () => _toDoService.CountAsync(userId));

            var todos = await _toDoService.GetPageAsync(userId, page, pageSize);
            return Ok(todos);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ToDoRestModel toDoViewModel)
        {
            var todo = await _toDoService.CreateAsync(toDoViewModel, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return CreatedAtAction("GET", todo.Id, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, ToDoRestModel toDoViewModel)
        {
            if (id != toDoViewModel.Id)
            {
                return BadRequest();
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var todo = await _toDoService.UpdateAsync(toDoViewModel, userId);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _toDoService.DeleteAsync(Id, userId);
            return Ok();
        }
    }
}
