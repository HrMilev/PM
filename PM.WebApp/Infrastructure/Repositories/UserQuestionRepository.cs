using PM.Common.Models.Rest;
using PM.WebApp.Infrastructure.Repositories.Interfaces;
using PM.WebApp.Infrastructure.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Repositories
{
    public class UserQuestionRepository : IUserQuestionRepository
    {
        private const string URL = "api/contactus";
        private readonly IHttpService _httpService;

        public UserQuestionRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<bool> CreateAsync(UserQuestionRestModel userQuestionRestModel)
        {
            var response = await _httpService.PostAsync<UserQuestionRestModel, object>(URL, userQuestionRestModel);

            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyAsync());
            }

            return response.IsSuccess;
        }

        public async Task<(IEnumerable<UserQuestionRestModel>, int)> GetPageAsync(int page, int pageSize = 5)
        {
            var response = await _httpService.GetPageableAsync<IEnumerable<UserQuestionRestModel>>(URL, page, pageSize);

            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyAsync());
            }

            int.TryParse(response.GetHeaderValues("X-Pages")[0], out int pages);

            return (response.Response, pages);
        }

        public async Task<string> DeleteAsync(int id)
        {
            string url = URL + $"/{id}";
            var response = await _httpService.DeleteAsync<string>(url);
            return response.Response;
        }

        public async Task<UserQuestionRestModel> UpdateAsync(UserQuestionRestModel userQuestion)
        {
            var uri = $"{URL}/{userQuestion.Id}";
            var response = await _httpService.UpdateAsync(uri, userQuestion);


            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyAsync());
            }

            return response.Response;
        }
    }
}
