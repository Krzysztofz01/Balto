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
            return entities
                .Include(p => p.Tabels).ThenInclude(p => p.Entries)
                .Include(p => p.Owner)
                .Include(p => p.ReadWriteUsers).ThenInclude(p => p.User)
                .Where(p => p.OwnerId == userId || p.ReadWriteUsers.Any(u => u.UserId == userId));
        }

        public async Task<bool> IsOwner(long projectId, long userId)
        {
            return await entities
                .AnyAsync(n => n.Id == projectId && n.OwnerId == userId);
        }

        public async Task<Project> SingleUsersProject(long projectId, long userId)
        {
            return await entities
                .Include(p => p.Tabels).ThenInclude(p => p.Entries)
                .Include(p => p.Owner)
                .Include(p => p.ReadWriteUsers).ThenInclude(p => p.User)
                .Where(p => p.OwnerId == userId || p.ReadWriteUsers.Any(u => u.UserId == userId))
                .SingleOrDefaultAsync(p => p.Id == projectId);
        }

        public async Task<Project> SingleUsersProjectOwner(long projectId, long userId)
        {
            return await entities
                .Include(p => p.Tabels).ThenInclude(p => p.Entries)
                .Include(p => p.Owner)
                .Include(p => p.ReadWriteUsers).ThenInclude(p => p.User)
                .Where(p => p.OwnerId == userId)
                .SingleOrDefaultAsync(p => p.Id == projectId);
        }
    }
}
