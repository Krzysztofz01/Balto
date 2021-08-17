using Balto.Application.Settings;
using Balto.Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.Web.Initializers
{
    public static class DatabaseInitializer
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = configuration
                .GetSection(nameof(DatabaseSettings))
                .Get<DatabaseSettings>();

            services.AddDbContext<BaltoDbContext>(opt =>
                opt.UseSqlServer(databaseSettings.SqlServerConnectionString));

            return services;
        }
    }
}
