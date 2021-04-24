using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using PM.Application.Interfaces.Services;
using AutoMapper;
using PM.Domain;
using System.Linq;
using PM.Common.Models.Rest;

namespace PM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly IUploadedFileService _uploadedFileService;
        private readonly IMapper _mapper;

        public FileController(IUploadedFileService uploadedFileService, IMapper mapper)
        {
            _uploadedFileService = uploadedFileService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var file = await _uploadedFileService.DownloadAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value, id);

            if (file.File == null)
            {
                return NotFound();
            }
            var restFile = _mapper.Map<UploadedFileRestModel>(file.File);
            restFile.Content = file.Content;
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
            var updatedFile = await _uploadedFileService.UpdateAsync(userId, _mapper.Map<UploadedFile>(file));
            if (updatedFile == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UploadedFileRestModel>(updatedFile));
        }

        [HttpPost("folder/{folderId}")]
        public async Task<IActionResult> Post(int folderId, [FromBody] IList<UploadedFileRestModel> uploadedFileRestModels)
        {
            var savedFiles = await _uploadedFileService.SaveFilesToFolder(User.FindFirst(ClaimTypes.NameIdentifier).Value, folderId, uploadedFileRestModels.Select(x => (_mapper.Map<UploadedFile>(x), x.Content)).ToList());
            if (savedFiles == null || savedFiles.Count != uploadedFileRestModels.Count)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<IList<UploadedFileRestModel>>(savedFiles));
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
