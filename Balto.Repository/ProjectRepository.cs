using Balto.Domain;
using Balto.Repository.Context;

namespace Balto.Repository
{
    class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(BaltoDbContext context) : base(context)
        {
        }
    }
}
