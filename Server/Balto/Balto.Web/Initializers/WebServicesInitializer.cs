using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Balto.Web.Initializers
{
    public static class WebServicesInitializer
    {
        public static IServiceCollection ConfigureWebServices(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Balto Web API", Version = "v1" });
            });

            services.AddHttpContextAccessor();

            services.AddHttpClient();
            
            services.AddControllers();

            return services;
        }
    }
}
