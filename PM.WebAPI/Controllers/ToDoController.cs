using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Application.Interfaces.Services;
using PM.Common.Models.Rest;
using PM.Domain;
using PM.WebAPI.Extensions;

namespace PM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;
        private readonly IMapper _mapper;

        public ToDoController(IToDoService toDoService, IMapper mapper)
        {
            _toDoService = toDoService;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<ToDoRestModel>>> Get([FromQuery] int page)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var pageSize = await Request.GetPageSizePagination(Response, () => _toDoService.CountAsync(userId));

            var todos = await _toDoService.GetPageAsync(userId, page, pageSize);
            return Ok(_mapper.Map<IList<ToDoRestModel>>(todos));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ToDoRestModel toDoViewModel)
        {
            var todo = await _toDoService.CreateAsync(_mapper.Map<ToDo>(toDoViewModel), User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return CreatedAtAction("GET", todo.Id.ToString(), _mapper.Map<ToDoRestModel>(todo));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, ToDoRestModel toDoViewModel)
        {
            if (id != toDoViewModel.Id)
            {
                return BadRequest();
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var todo = await _toDoService.UpdateAsync(_mapper.Map<ToDo>(toDoViewModel), userId);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ToDoRestModel>(todo));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!await _toDoService.DeleteAsync(id, userId))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
