using Balto.Application.Abstraction;
using Balto.Application.Goals;
using Balto.Application.Identities;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.API.Configuration
{
    public static class ApplicationServiceConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IGoalService, GoalService>();
            services.AddScoped<IIdentityService, IdentityService>();

            return services;
        }
    }
}
