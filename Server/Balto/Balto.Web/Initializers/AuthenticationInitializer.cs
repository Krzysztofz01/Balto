using Balto.Application.Authentication;
using Balto.Application.Settings;
using Balto.Infrastructure.Abstraction;
using Balto.Infrastructure.SqlServer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Balto.Web.Initializers
{
    public static class AuthenticationInitializer
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

            services.AddScoped<IAuthenticationHandler, AuthenticationHandler>();

            services.AddScoped<AuthenticationService>();

            var jwtSettingsSection = configuration.GetSection(nameof(JsonWebTokenSettings));
            services.Configure<JsonWebTokenSettings>(jwtSettingsSection);

            var jwtSettings = jwtSettingsSection.Get<JsonWebTokenSettings>();
            var secret = Encoding.ASCII.GetBytes(jwtSettings.TokenSecret);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
