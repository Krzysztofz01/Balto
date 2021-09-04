using Balto.Domain.Aggregates.Objective;
using Balto.Domain.Aggregates.User;
using Balto.Infrastructure.SqlServer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.Web.Initializers
{
    public static class EntityRepositoriesInitializer
    {
        public static IServiceCollection AddEntityRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IObjectiveRepository, ObjectiveRepository>();

            return services;
        }
    }
}
