using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PM.Common.Models.Rest;
using PM.Data.Entities;
using PM.WebAPI.Services.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactUsFormController : ControllerBase
    {
        private readonly IContactUsFormService _contactUsFormService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContactUsFormController(IContactUsFormService contactUsFormService,
            UserManager<ApplicationUser> userManager)
        {
            _contactUsFormService = contactUsFormService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ContactUsFormRestModel contactUsFormRestModel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);
            var userEmail = user.Email;
            var contactUs = await _contactUsFormService.CreateAsync(contactUsFormRestModel, userId, userEmail);
            if (contactUs == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
