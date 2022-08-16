using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Balto.Infrastructure.MySql.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMySqlInfrastructure(this IApplicationBuilder app, IServiceProvider service)
        {
            using var dbContext = service.GetRequiredService<BaltoDbContext>();

            dbContext.Database.Migrate();

            return app;
        }
    }
}
