using Microsoft.Extensions.Configuration;
using PM.Application.Interfaces.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Threading.Tasks;

namespace PM.Infrastructure
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendSuccessfulAsync((string email, string name) from, (string email, string name) to, string subject, string textMessage)
        {
            var apiKey = _configuration["SendGrid:APIKey"];
            var client = new SendGridClient(apiKey);
            var fromAddress = new EmailAddress(from.email, from.name);
            var toAddress = new EmailAddress(to.email, to.name);
            var msg = MailHelper.CreateSingleEmail(fromAddress, toAddress, subject, textMessage, textMessage);
            var response = await client.SendEmailAsync(msg);
            return response.StatusCode == HttpStatusCode.Accepted;
        }
    }
}
