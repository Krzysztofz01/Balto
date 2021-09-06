using Balto.Application.Authorization;
using Balto.Infrastructure.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.Web.Initializers
{
    public static class AuthorizationInitializer
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddScoped<IRequestAuthorizationHandler, RequestAuthorizationHandler>();

            return services;
        }
    }
}
