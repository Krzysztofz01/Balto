using Balto.Application.Email;
using Balto.Application.Monitoring;
using Balto.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Balto.Web.Initializers
{
    public static class UtilityServicesInitializer
    {
        public static IServiceCollection AddUtilityServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Monitoring
            services.AddScoped<IMonitoringService, MonitoringService>();

            //Email service
            var smtpSettings = configuration
                .GetSection(nameof(EmailSmtpSettings))
                .Get<EmailSmtpSettings>();

            services
                .AddFluentEmail(smtpSettings.Address)
                .AddSmtpSender(smtpSettings.Host, (string.IsNullOrEmpty(smtpSettings.Port) ? 587 : Convert.ToInt32(smtpSettings.Port)), smtpSettings.Login, smtpSettings.Password);

            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
