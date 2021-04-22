using Balto.Domain;
using Balto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public class ProjectTableEntryRepository : Repository<ProjectTableEntry>, IProjectTableEntryRepository
    {
        public ProjectTableEntryRepository(BaltoDbContext context) : base(context)
        {
        }

        public IEnumerable<ProjectTableEntry> AllUsersEntries(long projectId, long projectTableId, long userId)
        {
            return entities.Where(e => e.ProjectTableId == projectTableId && e.ProjectTable.ProjectId == projectId && e.ProjectTable.Project.OwnerId == userId);
        }

        public async Task<long> GetEntryOrder(long projectTableId)
        {
            var tableEntries = entities.Where(e => e.ProjectTableId == projectTableId);
            if (!tableEntries.Any()) return 0;

            return await tableEntries.MaxAsync(e => e.Order) + 1;
        }

        public async Task<ProjectTableEntry> SingleUsersEntry(long projectId, long projectTableId, long projectTableEntryId, long userId)
        {
            return await entities.SingleOrDefaultAsync(e => e.ProjectTableId == projectTableId && e.ProjectTable.ProjectId == projectId && e.ProjectTable.Project.OwnerId == userId && e.Id == projectTableEntryId);
        }
    }
}
