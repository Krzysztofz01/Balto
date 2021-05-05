using Balto.Domain;
using Balto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
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
            return entities
                .Include(e => e.UserAdded)
                .Include(e => e.UserFinished)
                .Include(e => e.ProjectTable).ThenInclude(e => e.Project).ThenInclude(e => e.Owner)
                .Include(e => e.ProjectTable).ThenInclude(e => e.Project).ThenInclude(e => e.ReadWriteUsers)
                .Where(e => e.ProjectTable.Project.OwnerId == userId || e.ProjectTable.Project.ReadWriteUsers.Any(u => u.UserId == userId))
                .Where(e => e.ProjectTableId == projectTableId && e.ProjectTable.ProjectId == projectId);
        }

        public async Task<long> GetEntryOrder(long projectTableId)
        {
            var tableEntries = entities.Where(e => e.ProjectTableId == projectTableId);
            if (!tableEntries.Any()) return 0;

            return await tableEntries.MaxAsync(e => e.Order) + 1;
        }

        public IEnumerable<ProjectTableEntry> IncomingEntriesDay()
        {
            /*return entities
                .Include(e => e.ProjectTable).ThenInclude(e => e.Project).ThenInclude(e => e.Owner)
                .Include(e => e.UserAdded)
                .Where(e => e.Finished == false && e.EndingDate <= DateTime.Now.AddDays(1.0));*/
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectTableEntry> IncomingEntriesWeek()
        {
            /*return entities
                .Include(e => e.ProjectTable).ThenInclude(e => e.Project).ThenInclude(e => e.Owner)
                .Include(e => e.UserAdded)
                .Where(e => e.Finished == false && e.EndingDate <= DateTime.Now.AddDays(1.0));*/
            throw new NotImplementedException();
        }

        public async Task<ProjectTableEntry> SingleUsersEntry(long projectId, long projectTableId, long projectTableEntryId, long userId)
        {
            return await entities
                .Include(e => e.UserAdded)
                .Include(e => e.UserFinished)
                .Include(e => e.ProjectTable).ThenInclude(e => e.Project).ThenInclude(e => e.Owner)
                .Include(e => e.ProjectTable).ThenInclude(e => e.Project).ThenInclude(e => e.ReadWriteUsers)
                .Where(e => e.ProjectTable.Project.OwnerId == userId || e.ProjectTable.Project.ReadWriteUsers.Any(u => u.UserId == userId))
                .SingleOrDefaultAsync(e => e.ProjectTableId == projectTableId && e.ProjectTable.ProjectId == projectId && e.Id == projectTableEntryId);
        }

        public async Task<ProjectTableEntry> SingleUsersEntryOwner(long projectId, long projectTableId, long projectTableEntryId, long userId)
        {
            return await entities
                .Include(e => e.UserAdded)
                .Include(e => e.UserFinished)
                .Include(e => e.ProjectTable).ThenInclude(e => e.Project).ThenInclude(e => e.Owner)
                .Include(e => e.ProjectTable).ThenInclude(e => e.Project).ThenInclude(e => e.ReadWriteUsers)
                .Where(e => e.ProjectTable.Project.OwnerId == userId)
                .SingleOrDefaultAsync(e => e.ProjectTableId == projectTableId && e.ProjectTable.ProjectId == projectId && e.Id == projectTableEntryId);
        }
    }
}
