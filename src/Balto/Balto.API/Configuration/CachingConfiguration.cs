using Microsoft.Extensions.DependencyInjection;

namespace Balto.API.Configuration
{
    public static class CachingConfiguration
    {
        public static IServiceCollection AddCaching(this IServiceCollection services)
        {
            services.AddMemoryCache();

            return services;
        }
    }
}
