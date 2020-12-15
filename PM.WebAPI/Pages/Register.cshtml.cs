using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using PM.Localizations;
using PM.WebAPI.Models.View;
using PM.Data.Entities;

namespace PM.WebAPI.Pages
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IStringLocalizer<Localization> _localizer;
        private readonly ICaptchaValidator _captchaValidator;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            ILogger<RegisterModel> logger,
            IStringLocalizer<Localization> localizer,
            ICaptchaValidator captchaValidator)
        {
            _userManager = userManager;
            _logger = logger;
            _localizer = localizer;
            _captchaValidator = captchaValidator;
        }

        [BindProperty]
        public RegisterViewModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (!await _captchaValidator.IsCaptchaPassedAsync(Input.Captcha))
            {
                ModelState.AddModelError("captcha", _localizer["Captcha validation failed"]);
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Username, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    return RedirectToPage("/Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
