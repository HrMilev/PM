using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
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

        public async Task<ActionResult<IEnumerable<ToDoRestModel>>> Get([FromQuery] int page)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int pageSize = 0;
            if (Request.Headers.TryGetValue("X-PageSize", out StringValues values)
                && int.TryParse(values.ToArray()[0], out pageSize))
            {
                var count = await _toDoService.CountAsync(userId);
                Response.Headers.Add("X-Pages", Math.Ceiling(((decimal)count) / pageSize).ToString("0"));
            }

            var todos = await _toDoService.GetPage(userId, page, pageSize);
            return Ok(todos);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ToDoRestModel toDoViewModel)
        {
            var todo = await _toDoService.CreateAsync(toDoViewModel, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return CreatedAtAction("GET", todo.Id, todo);
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
