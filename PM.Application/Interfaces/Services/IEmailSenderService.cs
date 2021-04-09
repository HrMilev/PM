using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Application.Interfaces.Services
{
    public interface IEmailSenderService
    {
        Task<bool> SendSuccessfulAsync((string email, string name) from, (string email, string name) to, string subject, string textMessage);
    }
}
