using Balto.API.Converters;
using Balto.API.Middleware;
using Balto.Application.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;

namespace Balto.API.Configuration
{
    public static class WebUtilitiesConfiguration
    {
        public static readonly string _corsPolicyName = "DefaultCorsPolicy";

        public static IServiceCollection AddWebUtilities(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            var swaggerSettingsSection = configuration.GetSection(nameof(SwaggerSettings));
            services.Configure<SwaggerSettings>(swaggerSettingsSection);

            var swaggerSettings = swaggerSettingsSection.Get<SwaggerSettings>();
            if (swaggerSettings.Enabled)
            {
                services.AddSwaggerGen(options =>
                {
                    var apiInfo = new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Balto Web API"
                    };

                    var securityDefinition = new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT"
                    };

                    var securityRequirements = new OpenApiSecurityRequirement
                    {
                        { securityDefinition, Array.Empty<string>() }
                    };

                    options.SwaggerDoc("v1", apiInfo);
                    options.CustomSchemaIds(type => type.FullName.Replace('+', '_'));
                    options.AddSecurityDefinition("Bearer", securityDefinition);
                    options.AddSecurityRequirement(securityRequirements);
                });
            }

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes;
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddHttpContextAccessor();

            services.AddCors(options =>
            {
                options.AddPolicy(_corsPolicyName, builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(options => true)
                        .AllowCredentials();
                });
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new AntiXssConverter());
            });

            return services;
        }

        public static IApplicationBuilder UseWebUtilities(this IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider service)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerSettings = service.GetService<IOptions<SwaggerSettings>>().Value ??
                throw new InvalidOperationException("Swagger settubgs are not available.");

            if (swaggerSettings.Enabled)
            {
                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }

            app.UseResponseCompression();

            app.UseMiddleware<ExceptionMiddleware>();

            // Xss middleware disabled due to false-positive problems.
            // AntiXss is now handled by custom converter. 
            // app.UseMiddleware<AntiXssMiddleware>();

            app.UseRouting();

            app.UseCors(_corsPolicyName);

            return app;
        }
    }
}
