using Balto.Domain;
using Balto.Repository.Context;

namespace Balto.Repository
{
    class ProjectTableEntryRepository : Repository<ProjectTableEntry>, IProjectTableEntryRepository
    {
        public ProjectTableEntryRepository(BaltoDbContext context) : base(context)
        {
        }
    }
}
