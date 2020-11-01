using PM.WebApp.Infrastructure.Utils.Interfaces;
using PM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Utils
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T data)
        {
            var jsonData = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonData, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await _httpClient.PostAsync(url, content);
            return new HttpResponseWrapper<object>(response.IsSuccessStatusCode, null, response);
        }
    }
}
