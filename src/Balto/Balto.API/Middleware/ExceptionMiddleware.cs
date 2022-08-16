﻿using Balto.API.Extensions;
using Balto.API.Services;
using Balto.Application.Plugin.Core;
using Balto.Domain.Core.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
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
        private readonly IHealthStatusService _healthStatusService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHealthStatusService healthStatusService,
            IWebHostEnvironment webHostEnvironment)
        {
            _next = next ??
                throw new ArgumentNullException(nameof(next));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _healthStatusService = healthStatusService ??
                throw new ArgumentNullException(nameof(healthStatusService));

            _webHostEnvironment = webHostEnvironment ??
                throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogDebug("Client with address: {IpAddress} requested resource under: {Url}",
                    context.Request.GetCurrentIp(),
                    context.Request.GetCurrentUri());

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
                BadHttpRequestException _ => (int)HttpStatusCode.BadRequest,
                PluginException _ => (int)HttpStatusCode.BadRequest,
                SystemAuthorizationException _ => (int)HttpStatusCode.Forbidden,

                _ => (int)HttpStatusCode.InternalServerError
            };

            if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
            {
                _healthStatusService.SetHealthStatusDegraded();
                _logger.LogError("Client with address: {IpAddress} requested resource under: {Url} causing a failure.",
                    context.Request.GetCurrentIp(),
                    context.Request.GetCurrentUri());

                _logger.LogError(ex, "Exception message: {Message}", ex.Message);
            }
            else
            {
                _logger.LogWarning(ex, "Exception message: {Message}", ex.Message);
            }

            var serializedResponse = JsonSerializer.Serialize(new
            {
                context.Response.StatusCode,
                Message = _webHostEnvironment.IsDevelopment() ? ex.Message : string.Empty
            });

            await context.Response.WriteAsync(serializedResponse);
        }
    }
}
