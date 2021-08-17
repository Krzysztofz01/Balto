using Balto.Application.Aggregates.User;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.Web.Initializers
{
    public static class EntityServicesInitializer
    {
        public static IServiceCollection AddEntityServices(this IServiceCollection services)
        {
            services.AddScoped<UserService>();

            return services;
        }
    }
}
