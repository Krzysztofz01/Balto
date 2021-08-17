using Balto.Application.Extensions;
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
        private readonly ILogger _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger logger)
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
                _logger.LogDebug(context.Request.GetUri().ToString());
                await _next(context);
            }
            catch(Exception e)
            {
                _logger.LogError($"Failure on: {e}");
                await HandleException(context, e);
            }
        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            switch(exception)
            {
                case ArgumentNullException _:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case ArgumentException _:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case InvalidOperationException _:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case InvalidEntityStateException _:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case InvalidValueObjectException _:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = exception?.Message, code = context.Response.StatusCode });

            await context.Response.WriteAsync(result);
        }
    }
}
