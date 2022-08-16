using Balto.Application.Settings;
using Balto.Infrastructure.MySql.Extensions;
using Balto.Infrastructure.PostgreSQL.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Balto.API.Configuration
{
    public static class PersistenceConfiguration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = configuration
                .GetSection(nameof(DatabaseSettings))
                .Get<DatabaseSettings>();

            // MySQL
            services.AddMySqlInfrastructure(databaseSettings.ConnectionString);

            // PostgreSQL
            // services.AddPostgreSQLInfrastructure(databaseSettings.ConnectionString);

            return services;
        }

        public static IApplicationBuilder UsePersistance(this IApplicationBuilder app, IServiceProvider service)
        {
            // MySQL
            app.UseMySqlInfrastructure(service);

            // PostgreSQL
            // app.UsePostgreSQLInfrastructure(service);

            return app;
        }
    }
}
