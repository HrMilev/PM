using PM.Common.Models.Rest;
using PM.WebApp.Infrastructure.Repositories.Interfaces;
using PM.WebApp.Infrastructure.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Create(ToDoRestModel toDo)
        {
            var response = await _httpService.PostAsync(URL, toDo);

            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyAsync());
            }
        }
    }
}
