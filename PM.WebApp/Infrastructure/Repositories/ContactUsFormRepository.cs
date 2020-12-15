using PM.Common.Models.Rest;
using PM.WebApp.Infrastructure.Repositories.Interfaces;
using PM.WebApp.Infrastructure.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Repositories
{
    public class ContactUsFormRepository : IContactUsFormRepository
    {
        private const string URL = "api/contactusform";
        private readonly IHttpService _httpService;

        public ContactUsFormRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<bool> CreateAsync(ContactUsFormRestModel contactUsFormRestModel)
        {
            var response = await _httpService.PostAsync<ContactUsFormRestModel, object>(URL, contactUsFormRestModel);

            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyAsync());
            }

            return response.IsSuccess;
        }
    }
}
