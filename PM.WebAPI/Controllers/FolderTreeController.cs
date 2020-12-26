using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Common.Models.Rest;
using PM.WebAPI.Services;

namespace PM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FolderTreeController : ControllerBase
    {
        private readonly IFolderService _folderService;

        public FolderTreeController(IFolderService folderService)
        {
            _folderService = folderService;
        }

        [HttpGet]
        public async Task<ActionResult<FolderRestModel>> Get()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var folder = await _folderService.GetTreeAsync(userId);
            return Ok(folder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FolderRestModel folder)
        {
            if (id != folder.Id)
            {
                return BadRequest();
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var updatedFolder = await _folderService.RenameAsync(id, userId, folder.Name);
            if (updatedFolder == null)
            {
                return NotFound();
            }

            return Ok(updatedFolder);
        }


        [HttpPost]
        public async Task<ActionResult<FolderRestModel>> Post(FolderRestModel folder)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var savedFolder = await _folderService.CreateFolderAsync(userId, folder);
            if (savedFolder == null)
            {
                return Conflict();
            }

            return CreatedAtAction("POST", savedFolder.Id, savedFolder);
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
