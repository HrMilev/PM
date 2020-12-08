using PM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Utils.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResponseWrapper<T>> GetAsync<T>(string url);
        Task<HttpResponseWrapper<TResponse>> PostAsync<T, TResponse>(string url, T data);
        Task<HttpResponseWrapper<T>> GetPageableAsync<T>(string url, int page, int pageSize);
        Task<HttpResponseWrapper<T>> DeleteAsync<T>(string url);
    }
}
