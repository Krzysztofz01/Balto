using Microsoft.Extensions.DependencyInjection;

namespace Balto.Web.Initializers
{
    public static class MapperInitializer
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {

            });

            return services;
        }
    }
}
