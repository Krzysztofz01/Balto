using Balto.Domain;
using Balto.Repository.Context;

namespace Balto.Repository
{
    public class ObjectiveRepository : Repository<Objective>, IObjectiveRepository
    {
        public ObjectiveRepository(BaltoDbContext context) : base(context)
        {
        }
    }
}
