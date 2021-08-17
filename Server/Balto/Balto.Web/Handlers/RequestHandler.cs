using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Balto.Web.Handlers
{
    public static class RequestHandler
    {
        public static async Task<IActionResult> HandleCommand<TRequest>(TRequest request, Func<TRequest, Task> handler)
        {
            await handler(request);
            
            return new OkResult();
        }

        public static async Task<IActionResult> HandleResultCommand<TRequest, TResponse>(TRequest request, Func<TRequest, Task<TResponse>> handler)
        {
            var result = await handler(request);

            return new OkObjectResult(result);
        }

        public static async Task<IActionResult> HandleQuery<TResponse>(Func<Task<TResponse>> query)
        {
            var result = await query();

            return new OkObjectResult(result);
        }

        public static async Task<IActionResult> HandleMappedQuery<TResponse, TResponseModel>(Func<Task<TResponse>> query, IMapper mapper, TResponseModel mappingType)
        {
            var result = mapper.Map<TResponseModel>(await query());

            return new OkObjectResult(result);
        }
    }
}
