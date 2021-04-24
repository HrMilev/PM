using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PM.Application.Interfaces.Repositories;
using PM.Application.Interfaces.Services;
using PM.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Application.Services
{
    public class UserQuestionService : IUserQuestionService
    {
        private readonly IUserQuestionRepository _userQuestionRepository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IConfiguration _configuration;

        public UserQuestionService(
            IUserQuestionRepository userQuestionRepository,
            IEmailSenderService emailSenderService,
            IConfiguration configuration)
        {
            _userQuestionRepository = userQuestionRepository;
            _emailSenderService = emailSenderService;
            _configuration = configuration;
        }

        public async Task<int> CountAsync(string userId = null)
        {
            return await _userQuestionRepository.GetQueryable().Where(x => x.UserResponderId == null).CountAsync();
        }

        public async Task<UserQuestion> CreateAsync(UserQuestion userQuestionRestModel, string creatorId, string creatorEmail)
        {
            var isSent = await _emailSenderService.SendSuccessfulAsync((_configuration["SendGrid:FromEmail"], _configuration["SendGrid:FromName"]),
                (_configuration["SendGrid:ToEmail"], _configuration["SendGrid:ToName"]),
                $"From {creatorEmail}: {userQuestionRestModel.Subject}",
                userQuestionRestModel.CreatorMessage);
            if (!isSent)
            {
                return null;
            }

            userQuestionRestModel.UserCreatorId = creatorId;
            userQuestionRestModel.CreateDate = DateTime.UtcNow;
            return await _userQuestionRepository.SaveAsync(userQuestionRestModel);
        }

        public async Task<IList<UserQuestion>> GetPageAsync(int page, int pageSize)
        {
            var userQuestions = await _userQuestionRepository.GetQueryable()
                .Where(x => x.UserResponderId == null)
                .OrderBy(x => x.CreateDate)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();
            return userQuestions;
        }

        public async Task DeleteAsync(int id)
        {
            await _userQuestionRepository.DeleteAsync(x => x.Id == id);
        }

        public async Task<UserQuestion> UpdateAsync(UserQuestion userQuestionRestModel, string replierId)
        {
            var oldUserQuestions = await _userQuestionRepository.GetAsync(userQuestionRestModel.Id);
            if (oldUserQuestions == null)
            {
                return null;
            }

            var isSent = await _emailSenderService.SendSuccessfulAsync((_configuration["SendGrid:FromEmail"], _configuration["SendGrid:FromName"]),
                    (_configuration["SendGrid:ToEmail"], _configuration["SendGrid:ToName"]),
                    $"Reply from {_configuration["SendGrid:FromEmail"]}: {userQuestionRestModel.Subject}",
                    userQuestionRestModel.ResponderMessage);
            if (!isSent)
            {
                return null;
            }

            userQuestionRestModel.UserResponderId = replierId;
            return await _userQuestionRepository.UpdateAsync(userQuestionRestModel);
        }
    }
}
