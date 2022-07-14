using Balto.Application.Abstraction;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.API.Configuration
{
    public static class ValidationConfiguration
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<ICommand>();

                ValidatorOptions.Global.LanguageManager.Enabled = false;
            });

            return services;
        }
    }
}
