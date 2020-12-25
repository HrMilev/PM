using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PM.Common.Models.Rest;
using PM.Data.Entities;
using PM.Data.Repositories.Interfaces;
using PM.WebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebAPI.Services
{
    public class UserQuestionService : IUserQuestionService
    {
        private readonly IMapper _mapper;
        private readonly IUserQuestionRepository _userQuestionRepository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IConfiguration _configuration;

        public UserQuestionService(IMapper mapper,
            IUserQuestionRepository userQuestionRepository,
            IEmailSenderService emailSenderService,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _userQuestionRepository = userQuestionRepository;
            _emailSenderService = emailSenderService;
            _configuration = configuration;
        }

        public async Task<int> CountAsync(string userId = null)
        {
            return await _userQuestionRepository.GetQueryable().Where(x => x.UserResponderId == null).CountAsync();
        }

        public async Task<UserQuestionRestModel> CreateAsync(UserQuestionRestModel userQuestionRestModel, string creatorId, string creatorEmail)
        {
            var isSent = await _emailSenderService.SendSuccessfulAsync((_configuration["SendGrid:FromEmail"], _configuration["SendGrid:FromName"]),
                (_configuration["SendGrid:ToEmail"], _configuration["SendGrid:ToName"]),
                $"From {creatorEmail}: {userQuestionRestModel.Subject}",
                userQuestionRestModel.CreatorMessage);
            if (!isSent)
            {
                return null;
            }

            var userQuestion = _mapper.Map<UserQuestion>(userQuestionRestModel);
            userQuestion.UserCreatorId = creatorId;
            userQuestion.CreateDate = DateTime.UtcNow;
            userQuestion = await _userQuestionRepository.SaveAsync(userQuestion);
            return _mapper.Map<UserQuestionRestModel>(userQuestion);
        }

        public async Task<IList<UserQuestionRestModel>> GetPageAsync(int page, int pageSize)
        {
            var userQuestions = await _userQuestionRepository.GetQueryable()
                .Where(x => x.UserResponderId == null)
                .OrderBy(x => x.CreateDate)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<IList<UserQuestionRestModel>>(userQuestions);
        }

        public async Task DeleteAsync(int id)
        {
            await _userQuestionRepository.DeleteAsync(x => x.Id == id);
        }

        public async Task<UserQuestionRestModel> UpdateAsync(UserQuestionRestModel userQuestionRestModel, string replierId)
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

            var userQuestion = _mapper.Map(userQuestionRestModel, oldUserQuestions);
            userQuestion.UserResponderId = replierId;
            await _userQuestionRepository.UpdateAsync(userQuestion);
            return _mapper.Map<UserQuestionRestModel>(userQuestion);
        }
    }
}
