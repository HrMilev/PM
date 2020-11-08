﻿using PM.Common.Models.Rest;
using PM.WebApp.Infrastructure.Repositories.Interfaces;
using PM.WebApp.Infrastructure.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private const string URL = "api/todo";
        private readonly IHttpService _httpService;

        public ToDoRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<ToDoRestModel>> GetToDos()
        {
            var response = await _httpService.GetAsync<IEnumerable<ToDoRestModel>>(URL);


            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyAsync());
            }

            return response.Response;
        }

        public async Task<ToDoRestModel> CreateAsync(ToDoRestModel toDo)
        {
            var response = await _httpService.PostAsync<ToDoRestModel, ToDoRestModel>(URL, toDo);

            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyAsync());
            }

            return response.Response;
        }
    }
}