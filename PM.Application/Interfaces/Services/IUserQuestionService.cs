using PM.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.Application.Interfaces.Services
{
    public interface IUserQuestionService : ICountableService
    {
        Task<IList<UserQuestion>> GetPageAsync(int page, int pageSize);
        Task<UserQuestion> CreateAsync(UserQuestion userQuestionRestModel, string creatorId, string creatorEmail);
        Task DeleteAsync(int id);
        Task<UserQuestion> UpdateAsync(UserQuestion userQuestionRestModel, string replierId);
    }
}
