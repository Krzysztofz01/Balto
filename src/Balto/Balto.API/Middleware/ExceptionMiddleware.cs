﻿using Balto.API.Extensions;
using Balto.Domain.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Balto.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next ??
                throw new ArgumentNullException(nameof(next));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogDebug(context.Request.GetCurrentUri().ToString());

                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = ex switch
            {
                InvalidOperationException _ => (int)HttpStatusCode.BadRequest,
                ArgumentException _ => (int)HttpStatusCode.BadRequest,
                BusinessLogicException _ => (int)HttpStatusCode.BadRequest,
                ValueObjectValidationException _ => (int)HttpStatusCode.BadRequest,
                SystemAuthorizationException _ => (int)HttpStatusCode.Forbidden,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            var serializedResponse = JsonSerializer.Serialize(new
            {
                context.Response.StatusCode,
                ex.Message
            });

            await context.Response.WriteAsync(serializedResponse);
        }
    }
}
