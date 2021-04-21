using Balto.Domain;
using Balto.Repository.Context;

namespace Balto.Repository
{
    public class ProjectTableRepository : Repository<ProjectTable>, IProjectTableRepository
    {
        public ProjectTableRepository(BaltoDbContext context) : base(context)
        {
        }
    }
}
