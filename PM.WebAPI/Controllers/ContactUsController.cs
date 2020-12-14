using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactUsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ContactUsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string message)
        {
            var apiKey = _configuration["SendGrid:APIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("i4koo@abv.bg", "Ico");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("hr.milev@yahoo.bg", "Icoo");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return Ok();
        }
    }
}
