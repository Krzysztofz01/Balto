using Balto.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.Web.Initializers
{
    public static class SettingsInitializer
    {
        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));

            services.Configure<UserRoleNameSettings>(configuration.GetSection(nameof(UserRoleNameSettings)));

            services.Configure<MonitoringSettings>(configuration.GetSection(nameof(MonitoringSettings)));

            services.Configure<EmailSmtpSettings>(configuration.GetSection(nameof(EmailSmtpSettings)));

            return services;
        }
    }
}
