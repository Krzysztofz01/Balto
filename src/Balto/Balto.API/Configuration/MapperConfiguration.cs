using Balto.Application.Goals;
using Balto.Application.Identities;
using Balto.Application.Notes;
using Balto.Application.Projects;
using Balto.Application.Tags;
using Balto.Application.Teams;
using Microsoft.Extensions.DependencyInjection;

namespace Balto.API.Configuration
{
    public static class MapperConfiguration
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(options =>
            {
                options.AddProfile<GoalMapperProfile>();
                options.AddProfile<IdentityMapperProfile>();
                options.AddProfile<ProjectMapperProfile>();
                options.AddProfile<TagMapperProfile>();
                options.AddProfile<NoteMapperProfile>();
                options.AddProfile<TeamMapperProfile>();
            });

            return services;
        }
    }
}
