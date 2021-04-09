using PM.Common.Models.Rest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.Application.Interfaces.Services
{
    public interface IUserQuestionService : ICountableService
    {
        Task<IList<UserQuestionRestModel>> GetPageAsync(int page, int pageSize);
        Task<UserQuestionRestModel> CreateAsync(UserQuestionRestModel userQuestionRestModel, string creatorId, string creatorEmail);
        Task DeleteAsync(int id);
        Task<UserQuestionRestModel> UpdateAsync(UserQuestionRestModel userQuestionRestModel, string replierId);
    }
}
