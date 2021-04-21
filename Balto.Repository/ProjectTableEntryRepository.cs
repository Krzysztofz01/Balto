using Balto.Domain;
using Balto.Repository.Context;
using System.Linq;

namespace Balto.Repository
{
    public class ProjectTableEntryRepository : Repository<ProjectTableEntry>, IProjectTableEntryRepository
    {
        public ProjectTableEntryRepository(BaltoDbContext context) : base(context)
        {
        }

        public long GetEntryOrder(long projectTableId)
        {
            long order = entities.Where(e => e.ProjectTableId == projectTableId).Max(e => e.Order);
            return order + 1;
        }
    }
}
