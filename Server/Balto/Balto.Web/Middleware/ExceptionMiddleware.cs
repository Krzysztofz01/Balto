﻿using Balto.Application.Extensions;
using Balto.Application.Telemetry;
using Balto.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Balto.Web.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly ITelemetryService _telemetryService;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            ITelemetryService telemetryService)
        {
            _next = next ??
                throw new ArgumentNullException(nameof(next));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _telemetryService = telemetryService ??
                throw new ArgumentNullException(nameof(telemetryService));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogDebug(context.Request.GetUri().ToString());
                await _next(context);
            }
            catch(Exception e)
            {
                await HandleException(context, e);
                await _telemetryService.LogException(e, context.Request.GetUri().ToString());
            }
        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            switch(exception)
            {
                case ArgumentNullException _:
                    _logger.LogWarning($"Failure on: {exception}");
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case ArgumentException _:
                    _logger.LogWarning($"Failure on: {exception}");
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case InvalidOperationException _:
                    _logger.LogWarning($"Failure on: {exception}");
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case InvalidEntityStateException _:
                    _logger.LogWarning($"Failure on: {exception}");
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case InvalidValueObjectException _:
                    _logger.LogWarning($"Failure on: {exception}");
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    _logger.LogError($"Failure on: {exception}");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = exception?.Message, code = context.Response.StatusCode });

            await context.Response.WriteAsync(result);
        }
    }
}
