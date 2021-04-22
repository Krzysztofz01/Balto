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
            return entities.Include(p => p.Entries).Where(p => p.ProjectId == projectId && p.Project.OwnerId == userId);
        }

        public async Task<ProjectTable> SingleUsersTable(long projectId, long tableId, long userId)
        {
            return await entities.Include(p => p.Entries).SingleOrDefaultAsync(p => p.ProjectId == projectId && p.Id == tableId && p.Project.OwnerId == userId);
        }
    }
}
