using PM.Common.Models.Rest;
using System.Threading.Tasks;

namespace PM.WebAPI.Services.Interfaces
{
    public interface IContactUsFormService
    {
        Task<ContactUsFormRestModel> CreateAsync(ContactUsFormRestModel contactUsFormRestModel, string creatorId, string creatorEmail);
    }
}
