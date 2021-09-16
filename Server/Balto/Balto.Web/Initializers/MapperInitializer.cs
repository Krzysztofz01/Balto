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
                cfg.AddProfile<Application.Aggregates.Note.MapperProfile>();
                cfg.AddProfile<Application.Aggregates.Project.MapperProfile>();
                cfg.AddProfile<Application.Aggregates.Team.MapperProfile>();
            });

            return services;
        }
    }
}
