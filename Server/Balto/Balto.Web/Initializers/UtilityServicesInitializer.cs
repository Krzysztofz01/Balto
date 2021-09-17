using Balto.Application.Email;
using Balto.Application.Settings;
using Balto.Application.Telemetry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Balto.Web.Initializers
{
    public static class UtilityServicesInitializer
    {
        public static IServiceCollection AddUtilityServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Telemetry and monitoring
            services.AddScoped<ITelemetryService, TelemetryService>();

            //Email service
            var smtpSettings = configuration
                .GetSection(nameof(EmailSmtpSettings))
                .Get<EmailSmtpSettings>();

            services
                .AddFluentEmail(smtpSettings.Address)
                .AddSmtpSender(smtpSettings.Host, Convert.ToInt32(smtpSettings.Port ?? "587"), smtpSettings.Login, smtpSettings.Password);

            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
