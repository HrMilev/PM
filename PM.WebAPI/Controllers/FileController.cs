using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PM.Common.Models.Rest;
using System.Collections.Generic;
using System.Security.Claims;
using PM.Application.Interfaces.Services;

namespace PM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly IUploadedFileService _uploadedFileService;

        public FileController(IUploadedFileService uploadedFileService)
        {
            _uploadedFileService = uploadedFileService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var file = await _uploadedFileService.DownloadAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value, id);

            if (file == null)
            {
                return NotFound();
            }

            return Ok(file);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UploadedFileRestModel file)
        {
            if (id != file.Id)
            {
                return BadRequest();
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var updatedFile = await _uploadedFileService.UpdateAsync(userId, file);
            if (updatedFile == null)
            {
                return NotFound();
            }

            return Ok(updatedFile);
        }

        [HttpPost("folder/{folderId}")]
        public async Task<IActionResult> Post(int folderId, [FromBody] IList<UploadedFileRestModel> uploadedFileRestModels)
        {
            var savedFiles = await _uploadedFileService.SaveFilesToFolder(User.FindFirst(ClaimTypes.NameIdentifier).Value, folderId, uploadedFileRestModels);
            if (savedFiles == null || savedFiles.Count != uploadedFileRestModels.Count)
            {
                return BadRequest();
            }

            return Ok(savedFiles);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!await _uploadedFileService.DeleteAsync(userId, id))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
