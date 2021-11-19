using Balto.Application.Abstraction;
using Balto.Application.Goals;
using Balto.Application.Identities;
using Balto.Application.Projects;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.API.Configuration
{
    public static class ApplicationServiceConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IGoalService, GoalService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IProjectService, ProjectService>();

            return services;
        }
    }
}
