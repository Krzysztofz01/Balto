using Balto.Infrastructure.Core.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.Infrastructure.PostgreSQL.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgreSQLInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BaltoDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IAuthenticationDataAccessService, AuthenticationDataAccessService>();

            services.AddScoped<IDataAccess, DataAccess>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
