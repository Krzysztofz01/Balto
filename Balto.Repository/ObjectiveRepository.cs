using Balto.Domain;
using Balto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public class ObjectiveRepository : Repository<Objective>, IObjectiveRepository
    {
        public ObjectiveRepository(BaltoDbContext context) : base(context)
        {
        }

        public IEnumerable<Objective> AllUsersObjectives(long userId)
        {
            return entities.Where(o => o.UserId == userId);
        }

        public async Task<Objective> SingleUsersObjective(long objectiveId, long userId)
        {
            return await entities.SingleOrDefaultAsync(o => o.Id == objectiveId && o.UserId == userId);
        }
    }
}
