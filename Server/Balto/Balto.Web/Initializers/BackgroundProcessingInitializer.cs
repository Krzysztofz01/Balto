using Balto.Application.Monitoring;
using Balto.Domain.Aggregates.Objective;
using Balto.Domain.Aggregates.Project;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Balto.Web.Initializers
{
    public static class BackgroundProcessingInitializer
    {
        public static IServiceCollection AddBackgroundProcessing(this IServiceCollection services)
        {
            services.AddHangfire(opt =>
            {
                opt.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseDefaultTypeSerializer()
                    .UseMemoryStorage();
            });

            return services;
        }

        public static IApplicationBuilder UseBackgroundProcessing(this IApplicationBuilder app, IRecurringJobManager rJob, IBackgroundJobClient bJob, IServiceProvider service)
        {
            app.UseHangfireServer();

            bJob.Schedule(() => service.GetService<IMonitoringService>().Ping(true), TimeSpan.FromSeconds(20));

            rJob.AddOrUpdate("Reset daily objectives",
                () => service.GetService<IObjectiveBackgroundProcessing>().ResetDailyObjectives(), Cron.Daily, TimeZoneInfo.Local);

            rJob.AddOrUpdate("Send project card deadline notification (one day)",
                () => service.GetService<IProjectBackgroundProcessing>().SendEmailNotificationsDayBefore(), Cron.Daily, TimeZoneInfo.Local);

            rJob.AddOrUpdate("Send project card deadline notification (three days)",
                () => service.GetService<IProjectBackgroundProcessing>().SendEmailNotificationsThreeDaysBefore(), Cron.Daily, TimeZoneInfo.Local);

            rJob.AddOrUpdate("Reset weekly objectives",
                () => service.GetService<IObjectiveBackgroundProcessing>().ResetWeeklyObjectives(), Cron.Daily, TimeZoneInfo.Local);

            rJob.AddOrUpdate("Ping to monitoring server",
                () => service.GetService<IMonitoringService>().Ping(false), Cron.Hourly, TimeZoneInfo.Local);

            rJob.AddOrUpdate("Instance status monitoring",
                () => service.GetService<IMonitoringService>().ReportInstanceStatus(), Cron.Daily, TimeZoneInfo.Local);

            return app;
        }
    }
}
