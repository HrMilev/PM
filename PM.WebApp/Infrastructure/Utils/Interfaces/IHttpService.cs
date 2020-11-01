using PM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Utils.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T data);
    }
}
