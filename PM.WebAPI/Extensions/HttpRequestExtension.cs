using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace PM.WebAPI.Extensions
{
    public static class HttpRequestExtension
    {
        public static async Task<int> GetPageSizePagination(this HttpRequest httpRequest, HttpResponse httpResponse, Func<Task<int>> totalCountAsync)
        {
            int pageSize = 0;
            if (httpRequest.Headers.TryGetValue("X-PageSize", out StringValues values)
                && int.TryParse(values.ToArray()[0], out pageSize) && pageSize > 0)
            {
                var count = await totalCountAsync();
                httpResponse.Headers.Add("X-Pages", Math.Ceiling(((decimal)count) / pageSize).ToString("0"));
            }

            return pageSize;
        }
    }
}
