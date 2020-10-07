using System.Threading.Tasks;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using PM.Localizations;
using PM.WebAPI.Models;
using PM.WebAPI.Models.View;

namespace PM.WebAPI.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IStringLocalizer<Localization> _localizer;
        private readonly ICaptchaValidator _captchaValidator;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            IStringLocalizer<Localization> localizer,
            ICaptchaValidator captchaValidator)
        {
            _signInManager = signInManager;
            _logger = logger;
            _localizer = localizer;
            _captchaValidator = captchaValidator;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public void OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            ReturnUrl = returnUrl ?? Url.Content("~/");
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
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
            }

            ModelState.AddModelError(string.Empty, _localizer["Invalid login attempt"]);

            return Page();
        }
    }
}
