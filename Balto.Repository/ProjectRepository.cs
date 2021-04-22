using Balto.Domain;
using Balto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(BaltoDbContext context) : base(context)
        {
        }

        public IEnumerable<Project> AllUsersProjects(long userId)
        {
            return entities.Include(p => p.Tabels).ThenInclude(p => p.Entries).Where(p => p.OwnerId == userId);
        }

        public async Task<Project> SingleUsersProject(long projectId, long userId)
        {
            return await entities.Include(p => p.Tabels).ThenInclude(p => p.Entries).SingleOrDefaultAsync(p => p.Id == projectId && p.OwnerId == userId);
        }
    }
}
