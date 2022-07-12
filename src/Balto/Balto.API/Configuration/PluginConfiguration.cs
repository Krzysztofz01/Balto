using Balto.Application.Plugin.TrelloIntegration.Extensions;
using Balto.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.API.Configuration
{
    public static class PluginConfiguration
    {
        public static IServiceCollection AddPlugins(this IServiceCollection services, IConfiguration configuration)
        {
            var generalPluginSettingsSection = configuration
                .GetSection(nameof(PluginSettings));

            services.Configure<PluginSettings>(generalPluginSettingsSection);
            var generalPluginSettings = generalPluginSettingsSection.Get<PluginSettings>();

            if (generalPluginSettings.Enabled)
            {
                services.AddTrelloIntegration(configuration);
            }

            return services;
        }
    }
}
