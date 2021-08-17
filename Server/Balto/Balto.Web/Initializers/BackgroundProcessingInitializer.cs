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

        public static IApplicationBuilder UseBackgroundProcessing(this IApplicationBuilder app, IRecurringJobManager job, IServiceProvider service)
        {
            app.UseHangfireServer();

            return app;
        }
    }
}
