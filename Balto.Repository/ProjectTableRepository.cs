using Balto.Domain;
using Balto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public class ProjectTableRepository : Repository<ProjectTable>, IProjectTableRepository
    {
        public ProjectTableRepository(BaltoDbContext context) : base(context)
        {
        }

        public IEnumerable<ProjectTable> AllUserTabels(long projectId, long userId)
        {
            return entities
                .Include(p => p.Entries).ThenInclude(p => p.UserAdded)
                .Include(p => p.Entries).ThenInclude(p => p.UserFinished)
                .Include(p => p.Project).ThenInclude(p => p.Owner)
                .Include(p => p.Project).ThenInclude(p => p.ReadWriteUsers).ThenInclude(p => p.User)
                .Where(p => p.Project.OwnerId == userId || p.Project.ReadWriteUsers.Any(u => u.UserId == userId))
                .Where(p => p.ProjectId == projectId);
        }

        public async Task<bool> Exist(long projectId, long tableId)
        {
            return await entities
                    .AnyAsync(p => p.ProjectId == projectId && p.Id == tableId);
        }

        public async Task<ProjectTable> SingleUsersTable(long projectId, long tableId, long userId)
        {
            return await entities
                .Include(p => p.Entries).ThenInclude(p => p.UserAdded)
                .Include(p => p.Entries).ThenInclude(p => p.UserFinished)
                .Include(p => p.Project).ThenInclude(p => p.Owner)
                .Include(p => p.Project).ThenInclude(p => p.ReadWriteUsers).ThenInclude(p => p.User)
                .Where(p => p.Project.OwnerId == userId || p.Project.ReadWriteUsers.Any(u => u.UserId == userId))
                .SingleOrDefaultAsync(p => p.ProjectId == projectId && p.Id == tableId);
        }

        public async Task<ProjectTable> SingleUsersTableOwner(long projectId, long tableId, long userId)
        {
            return await entities
                .Include(p => p.Entries).ThenInclude(p => p.UserAdded)
                .Include(p => p.Entries).ThenInclude(p => p.UserFinished)
                .Include(p => p.Project).ThenInclude(p => p.Owner)
                .Include(p => p.Project).ThenInclude(p => p.ReadWriteUsers).ThenInclude(p => p.User)
                .Where(p => p.Project.OwnerId == userId)
                .SingleOrDefaultAsync(p => p.ProjectId == projectId && p.Id == tableId);
        }
    }
}
