﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PM.Common.Models.Rest;
using PM.Common.Strings;
using PM.Domain;
using PM.WebAPI.Extensions;
using PM.WebAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactUsController : ControllerBase
    {
        private readonly IUserQuestionService _userQuestionService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContactUsController(IUserQuestionService userQuestionService,
            UserManager<ApplicationUser> userManager)
        {
            _userQuestionService = userQuestionService;
            _userManager = userManager;
        }

        [Authorize(Roles = UserRole.Admin)]
        public async Task<ActionResult<IEnumerable<UserQuestion>>> Get([FromQuery] int page)
        {
            var pageSize = await Request.GetPageSizePagination(Response, () => _userQuestionService.CountAsync());
            var contactUsForms = await _userQuestionService.GetPageAsync(page, pageSize);
            return Ok(contactUsForms);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserQuestionRestModel contactUsFormRestModel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);
            var userEmail = user.Email;
            var userQuestion = await _userQuestionService.CreateAsync(contactUsFormRestModel, userId, userEmail);
            if (userQuestion == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> Delete(int Id)
        {
            await _userQuestionService.DeleteAsync(Id);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> Put(int id, UserQuestionRestModel userQuestionRestModel)
        {
            if (userQuestionRestModel.Id.HasValue && id != userQuestionRestModel.Id)
            {
                return BadRequest();
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userQuestion = await _userQuestionService.UpdateAsync(userQuestionRestModel, userId);
            if (userQuestion == null)
            {
                return NotFound();
            }

            return Ok(userQuestion);
        }
    }
}
