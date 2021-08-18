using Balto.Web.Initializers;
using Balto.Web.Middleware;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Balto.Web
{
    public class Startup
    {
        private const string _corsPolicyName = "Default Policy";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSettings(Configuration);

            services.AddDatabase(Configuration);

            services.AddEntityRepositories();

            services.AddEntityServices();

            services.ConfigureAuthentication(Configuration);

            services.ConfigureAuthorization();

            services.ConfigureWebServices();

            services.AddBackgroundProcessing();

            services.AddMapper();

            services.AddCors(opt => opt.AddPolicy(_corsPolicyName, builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(opt => true)
                    .AllowCredentials();
            }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager job, IServiceProvider service)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(opt =>
                {
                    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Balto Web API v1");
                });

                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseCors(_corsPolicyName);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseBackgroundProcessing(job, service);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
