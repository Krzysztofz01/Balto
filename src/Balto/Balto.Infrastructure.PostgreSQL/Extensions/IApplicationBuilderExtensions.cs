using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Balto.Infrastructure.PostgreSQL.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UsePostgreSQLInfrastructure(this IApplicationBuilder app, IServiceProvider service)
        {
            using var dbContext = service.GetRequiredService<BaltoDbContext>();

            dbContext.Database.Migrate();

            return app;
        }
    }
}
