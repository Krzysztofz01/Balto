using Balto.Application.Plugin.TrelloIntegration.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.Application.Plugin.TrelloIntegration.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTrelloIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TrelloIntegrationSettings>(configuration
                .GetSection(nameof(TrelloIntegrationSettings)));

            services.AddScoped<ITrelloIntegration, TrelloIntegration>();

            return services;
        }
    }
}
