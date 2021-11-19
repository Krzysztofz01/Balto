using Balto.Application.Goals;
using Balto.Application.Identities;
using Balto.Application.Projects;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.API.Configuration
{
    public static class MapperConfiguration
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(options =>
            {
                options.AddProfile<GoalMapperProfile>();
                options.AddProfile<IdentityMapperProfile>();
                options.AddProfile<ProjectMapperProfile>();
            });

            return services;
        }
    }
}
