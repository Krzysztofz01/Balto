using Balto.API.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Balto.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMySqlPersistence(Configuration);

            services.AddAuthentication(Configuration);

            services.AddAuthorization(Configuration);

            services.AddApplicationServices();

            services.AddMapper();

            services.AddBackgroundJobs();

            services.AddCaching();

            services.AddServiceHealthChecks(Configuration);

            services.AddWebUtilities(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider service)
        {
            app.UseWebUtilities(env, service);

            app.UseMySqlPersistance(service);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseServiceHealthChecks(service);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
