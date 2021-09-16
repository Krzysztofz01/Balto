using Balto.Application.Aggregates.Note;
using Balto.Application.Aggregates.Objectives;
using Balto.Application.Aggregates.Project;
using Balto.Application.Aggregates.Team;
using Balto.Application.Aggregates.User;
using Balto.Domain.Aggregates.Objective;
using Balto.Domain.Aggregates.Project;
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

            services.AddScoped<ProjectService>();
            services.AddScoped<IProjectBackgroundProcessing, ProjectBackgroundProcessing>();

            services.AddScoped<TeamService>();

            return services;
        }
    }
}
