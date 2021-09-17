using Balto.Application.Integrations.Trello;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.Web.Initializers
{
    public static class IntegrationsInitializer
    {
        public static IServiceCollection AddIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            //Trello
            services.AddScoped<ITrelloIntegration, TrelloIntegration>();
            services.AddScoped<TrelloService>();

            return services;
        }
    }
}
