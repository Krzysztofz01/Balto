using Balto.Application.Extensions;
using Balto.Application.Goals;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Balto.API.Configuration
{
    public static class BackgroundJobConfiguration
    {
        private const string _schedulerId = "BaltoQuartz";

        public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
        {
            services.Configure<QuartzOptions>(options =>
            {
                options.Scheduling.IgnoreDuplicates = true;
                options.Scheduling.OverWriteExistingData = true;
            });

            services.AddQuartz(options =>
            {
                options.SchedulerId = _schedulerId;

                options.UseMicrosoftDependencyInjectionJobFactory();
                options.UseSimpleTypeLoader();
                options.UseInMemoryStore();
                options.UseDefaultThreadPool(tpool =>
                {
                    tpool.MaxConcurrency = 10;
                });

                options.AddJobAndTrigger<GoalRecurringResetJob>(
                    GoalRecurringResetJob.JobName,
                    GoalRecurringResetJob.CronExpression);
            });

            services.AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            return services;
        }
    }
}
