﻿using Balto.Application.Abstraction;
using Balto.Domain.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Balto.API.Controllers.Base
{
    [ApiController]
    [Authorize]
    public abstract class CommandController : ControllerBase
    {
        public CommandController()
        {
        }

        protected async Task<IActionResult> Command<TAggregateRoot>(IApplicationCommand<TAggregateRoot> command, Func<IApplicationCommand<TAggregateRoot>, Task> serviceHandler) where TAggregateRoot : AggregateRoot
        {
            await serviceHandler(command);

            return new OkResult();
        }

        protected async Task<IActionResult> ExecuteService<TRequest>(TRequest request, Func<TRequest, Task> serviceHandler) where TRequest : class
        {
            await serviceHandler(request);

            return new OkResult();
        }
    }
}
