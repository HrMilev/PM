﻿using PM.WebApp.Infrastructure.Utils.Interfaces;
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
        private readonly JsonSerializerOptions _defaultSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            IgnoreNullValues = true,
        };

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseWrapper<T>> GetAsync<T>(string url)
        {
            var responseHTTP = await _httpClient.GetAsync(url);
            return await GetWrapper<T>(responseHTTP);
        }

        public async Task<HttpResponseWrapper<TResponse>> PostAsync<T, TResponse>(string url, T data)
        {
            var jsonData = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonData, Encoding.UTF8, MediaTypeNames.Application.Json);
            var responseHTTP = await _httpClient.PostAsync(url, content);
            return await GetWrapper<TResponse>(responseHTTP);
        }

        private async Task<HttpResponseWrapper<T>> GetWrapper<T>(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var response = await Deserialize<T>(httpResponseMessage, _defaultSerializerOptions);
                return new HttpResponseWrapper<T>(true, response, httpResponseMessage);
            }
            else
            {
                return new HttpResponseWrapper<T>(false, default, httpResponseMessage);
            }

        }

        private async Task<T> Deserialize<T>(HttpResponseMessage message, JsonSerializerOptions options)
        {
            var responseAsString = await message.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseAsString, options);
        }
    }
}
