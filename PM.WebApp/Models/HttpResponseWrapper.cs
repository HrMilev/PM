using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PM.WebApp.Models
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(bool isSuccess, T response, HttpResponseMessage responseMessage)
        {
            IsSuccess = isSuccess;
            Response = response;
            ResponseMessage = responseMessage;
        }

        public bool IsSuccess { get; }
        public T Response { get; }
        public HttpResponseMessage ResponseMessage { get; }

        public async Task<string> GetBodyAsync()
        {
            return await ResponseMessage.Content.ReadAsStringAsync();
        }
    }
}
