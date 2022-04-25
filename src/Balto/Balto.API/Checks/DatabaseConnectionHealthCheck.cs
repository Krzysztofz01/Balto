using Balto.Infrastructure.MySql;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Balto.API.Checks
{
    internal class DatabaseConnectionHealthCheck : IHealthCheck
    {
        private readonly BaltoDbContext _dbContext;

        public DatabaseConnectionHealthCheck(BaltoDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (await _dbContext.Database.CanConnectAsync(cancellationToken))
                return HealthCheckResult.Healthy();

            return HealthCheckResult.Unhealthy();
        }
    }
}
