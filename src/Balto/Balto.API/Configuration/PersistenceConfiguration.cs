using Balto.Application.Settings;
using Balto.Infrastructure.Core.Abstraction;
using Balto.Infrastructure.MySql;
using Balto.Infrastructure.MySql.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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

            services.AddScoped<IDataAccess, DataAccess>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IApplicationBuilder UseMySqlPersistance(this IApplicationBuilder app, IServiceProvider service)
        {
            using var dbContext = service.GetRequiredService<BaltoDbContext>();

            dbContext.Database.Migrate();

            return app;
        }
    }
}
