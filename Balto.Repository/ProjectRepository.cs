using Balto.Domain;
using Balto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(BaltoDbContext context) : base(context)
        {
        }

        public async Task<Project> SingleUsersProject(long projectId, long userId)
        {
            return await entities.SingleOrDefaultAsync(p => p.Id == projectId && p.OwnerId == userId);
        }
    }
}
