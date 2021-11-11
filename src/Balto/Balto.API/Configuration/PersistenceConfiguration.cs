using Balto.Application.Settings;
using Balto.Domain.Goals;
using Balto.Domain.Identities;
using Balto.Domain.Projects;
using Balto.Infrastructure.Core.Abstraction;
using Balto.Infrastructure.MySql;
using Balto.Infrastructure.MySql.Extensions;
using Balto.Infrastructure.MySql.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.API.Configuration
{
    public static class PersistenceConfiguration
    {
        public static IServiceCollection AddMySqlPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = configuration
                .GetSection(nameof(DatabaseSettings))
                .Get<DatabaseSettings>();

            services.AddDbContext<BaltoDbContext>(options =>
                options.UseMySql(databaseSettings.ConnectionString));

            services.AddScoped<IAuthenticationDataAccessService, AuthenticationDataAccessService>();

            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            services.AddScoped<IDataAccess, DataAccess>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
