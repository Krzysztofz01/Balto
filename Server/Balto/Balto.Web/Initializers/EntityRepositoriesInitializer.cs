using Balto.Domain.Aggregates.Note;
using Balto.Domain.Aggregates.Objective;
using Balto.Domain.Aggregates.Project;
using Balto.Domain.Aggregates.Team;
using Balto.Domain.Aggregates.User;
using Balto.Infrastructure.SqlServer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.Web.Initializers
{
    public static class EntityRepositoriesInitializer
    {
        public static IServiceCollection AddEntityRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IObjectiveRepository, ObjectiveRepository>();

            services.AddScoped<INoteRepository, NoteRepository>();

            services.AddScoped<IProjectRepository, ProjectRepository>();

            services.AddScoped<ITeamRepository, TeamRepository>();

            return services;
        }
    }
}
