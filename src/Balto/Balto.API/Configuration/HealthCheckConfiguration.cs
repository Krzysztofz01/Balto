﻿using Balto.API.Checks;
using Balto.API.Services;
using Balto.Application.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Balto.API.Configuration
{
    public static class HealthCheckConfiguration
    {
        public const string _healthCheckPath = "/health";

        public static IServiceCollection AddServiceHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var healthCheckSettingsSection = configuration
                .GetSection(nameof(HealthCheckSettings));

            services.Configure<HealthCheckSettings>(healthCheckSettingsSection);
            var healthCheckSettings = healthCheckSettingsSection.Get<HealthCheckSettings>();

            services.AddSingleton<IHealthStatusService, HealthStatusService>();

            if (healthCheckSettings.Enabled)
            {
                services.AddHealthChecks()
                    .AddCheck<DatabaseConnectionHealthCheck>(nameof(DatabaseConnectionHealthCheck))
                    .AddCheck<UnexpectedExceptionHealthCheck>(nameof(UnexpectedExceptionHealthCheck));
            }

            return services;
        }

        public static IApplicationBuilder UseServiceHealthChecks(this IApplicationBuilder app, IServiceProvider service)
        {
            var healthCheckSettings = service.GetService<IOptions<HealthCheckSettings>>().Value ??
                throw new InvalidOperationException("Healthcheck settings are not available.");
        
            if (healthCheckSettings.Enabled)
            {
                app.UseHealthChecks(_healthCheckPath, new HealthCheckOptions
                {
                    AllowCachingResponses = false
                });
            }

            return app;
        }
    }
}
