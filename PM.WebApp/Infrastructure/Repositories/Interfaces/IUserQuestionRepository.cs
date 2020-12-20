using PM.Common.Models.Rest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Repositories.Interfaces
{
    public interface IUserQuestionRepository
    {
        Task<bool> CreateAsync(UserQuestionRestModel userQuestionRestModel);
        Task<(IEnumerable<UserQuestionRestModel>, int)> GetPageAsync(int page, int pageSize = 5);
        Task<string> DeleteAsync(int id);
        Task<UserQuestionRestModel> UpdateAsync(UserQuestionRestModel userQuestion);
    }
}
