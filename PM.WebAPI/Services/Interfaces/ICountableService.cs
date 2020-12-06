using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebAPI.Services.Interfaces
{
    public interface ICountableService
    {
        Task<int> CountAsync(string userId);
    }
}
