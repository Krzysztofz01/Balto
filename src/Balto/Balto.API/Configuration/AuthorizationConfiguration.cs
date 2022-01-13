using Balto.Application.Authorization;
using Balto.Application.Settings;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.API.Configuration
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthorizationSettings>(configuration.GetSection(nameof(AuthorizationSettings)));

            services.AddScoped<IScopeWrapperService, ScopeWrapperService>();

            return services;
        }
    }
}
