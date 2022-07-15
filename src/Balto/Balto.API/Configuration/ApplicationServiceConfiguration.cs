using Balto.Application.Abstraction;
using Balto.Application.Goals;
using Balto.Application.Identities;
using Balto.Application.Notes;
using Balto.Application.Projects;
using Balto.Application.Tags;
using Balto.Application.Teams;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.API.Configuration
{
    public static class ApplicationServiceConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IGoalService, GoalService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<ITeamService, TeamService>();

            return services;
        }
    }
}
