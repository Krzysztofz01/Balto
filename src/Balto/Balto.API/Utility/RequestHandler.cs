using AutoMapper;
using Balto.Application.Abstraction;
using Balto.Domain.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Balto.API.Utility
{
    public static class RequestHandler
    {
        public static async Task<IActionResult> Command<TAggregateRoot>(IApplicationCommand<TAggregateRoot> command, Func<IApplicationCommand<TAggregateRoot>, Task> serviceHandler) where TAggregateRoot : AggregateRoot
        {
            await serviceHandler(command);

            return new OkResult();
        }

        public static async Task<IActionResult> MapQuery<TResponse, TResponseDto>(Task<TResponse> query, IMapper mapper)
        {
            var result = await query;

            if (result is null) return new NotFoundResult();

            var mappedResult = mapper.Map<TResponseDto>(result);

            return new OkObjectResult(mappedResult);
        }
    }
}
