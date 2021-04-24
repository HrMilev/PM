using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Application.Interfaces.Services;
using PM.Common.Models.Rest;
using PM.Domain;

namespace PM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FolderTreeController : ControllerBase
    {
        private readonly IFolderService _folderService;
        private readonly IMapper _mapper;

        public FolderTreeController(IFolderService folderService, IMapper mapper)
        {
            _folderService = folderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<FolderRestModel>> Get()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var folder = await _folderService.GetTreeAsync(userId);
            return Ok(_mapper.Map<FolderRestModel>(folder));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FolderRestModel folder)
        {
            if (id != folder.Id)
            {
                return BadRequest();
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var updatedFolder = await _folderService.UpdateAsync(userId, _mapper.Map<Folder>(folder));
            if (updatedFolder == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FolderRestModel>(updatedFolder));
        }

        [HttpPost]
        public async Task<ActionResult<FolderRestModel>> Post(FolderRestModel folder)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var savedFolder = await _folderService.CreateFolderAsync(userId, _mapper.Map<Folder>(folder));
            if (savedFolder == null)
            {
                return Conflict();
            }

            return CreatedAtAction("POST", savedFolder.Id, _mapper.Map<FolderRestModel>(savedFolder));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _folderService.DeleteAsync(Id, userId);
            return Ok();
        }
    }
}
