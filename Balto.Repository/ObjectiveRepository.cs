using Balto.Domain;
using Balto.Repository.Context;

namespace Balto.Repository
{
    class ObjectiveRepository : Repository<Objective>, IObjectiveRepository
    {
        public ObjectiveRepository(BaltoDbContext context) : base(context)
        {
        }
    }
}
