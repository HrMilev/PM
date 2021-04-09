using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Application.Interfaces.Services
{
    public interface ICountableService
    {
        Task<int> CountAsync(string userId = null);
    }
}
