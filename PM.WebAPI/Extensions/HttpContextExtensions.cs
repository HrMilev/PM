using Microsoft.AspNetCore.Http;
using PM.Common.Models.Rest;
using PM.WebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebAPI.Extensions
{
    public static class HttpContextExtensions
    {
        public static async Task SetPaginationHeaders(this HttpContext httpContext, ICountableService countableService, PageableRestModel pageableRestModel)
        {
            if (httpContext == null || countableService == null || pageableRestModel == null)
            {
                throw new ArgumentNullException();
            }

            double count = await countableService.CountAsync();
            double pagesAmount = Math.Ceiling(count / pageableRestModel.EntitiesPerPage);
            httpContext.Response.Headers.Add("Pagination-Count", count.ToString());
            httpContext.Response.Headers.Add("Pagination-Page", pageableRestModel.Page.ToString());
            httpContext.Response.Headers.Add("Pagination-AmountOfPages", pagesAmount.ToString());
        }
    }
}
