using AutoMapper;
using Microsoft.Extensions.Configuration;
using PM.Common.Models.Rest;
using PM.Data.Entities;
using PM.Data.Repositories.Interfaces;
using PM.WebAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PM.WebAPI.Services
{
    public class ContactUsFormService : IContactUsFormService
    {
        private readonly IMapper _mapper;
        private readonly IContactUsFormRepository _contactUsFormRepository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IConfiguration _configuration;

        public ContactUsFormService(IMapper mapper,
            IContactUsFormRepository contactUsFormRepository,
            IEmailSenderService emailSenderService,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _contactUsFormRepository = contactUsFormRepository;
            _emailSenderService = emailSenderService;
            _configuration = configuration;
        }

        public async Task<ContactUsFormRestModel> CreateAsync(ContactUsFormRestModel contactUsFormRestModel, string creatorId, string creatorEmail)
        {
            var isSent = await _emailSenderService.SendSuccessfulAsync((_configuration["SendGrid:FromEmail"], _configuration["SendGrid:FromName"]),
                (_configuration["SendGrid:ToEmail"], _configuration["SendGrid:ToName"]),
                $"From {creatorEmail}: {contactUsFormRestModel.Subject}",
                contactUsFormRestModel.CreatorMessage);
            if (!isSent)
            {
                return null;
            }

            var contactUs = _mapper.Map<ContactUsForm>(contactUsFormRestModel);
            contactUs.UserCreatorId = creatorId;
            contactUs.CreateDate = DateTime.UtcNow;
            contactUs = await _contactUsFormRepository.SaveAsync(contactUs);
            return _mapper.Map<ContactUsFormRestModel>(contactUs);
        }
    }
}
