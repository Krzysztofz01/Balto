using Balto.Application.Aggregates.Note;
using Balto.Application.Aggregates.Objectives;
using Balto.Application.Aggregates.User;
using Balto.Domain.Aggregates.Objective;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.Web.Initializers
{
    public static class EntityServicesInitializer
    {
        public static IServiceCollection AddEntityServices(this IServiceCollection services)
        {
            services.AddScoped<UserService>();

            services.AddScoped<ObjectiveService>();
            services.AddScoped<IObjectiveBackgroundProcessing, ObjectiveBackgroundProcessing>();

            services.AddScoped<NoteService>();

            return services;
        }
    }
}
