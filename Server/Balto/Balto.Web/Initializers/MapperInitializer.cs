using Microsoft.Extensions.DependencyInjection;

namespace Balto.Web.Initializers
{
    public static class MapperInitializer
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<Application.Aggregates.User.MapperProfile>();
                cfg.AddProfile<Application.Aggregates.Objectives.MapperProfile>();
            });

            return services;
        }
    }
}
