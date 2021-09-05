using Balto.Application.Telemetry;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.Web.Initializers
{
    public static class UtilityServicesInitializer
    {
        public static IServiceCollection AddUtilityServices(this IServiceCollection services)
        {
            services.AddScoped<ITelemetryService, TelemetryService>();

            return services;
        }
    }
}
