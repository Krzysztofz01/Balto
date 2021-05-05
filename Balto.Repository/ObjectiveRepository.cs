using Balto.Domain;
using Balto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
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

        public IEnumerable<Objective> IncomingObjectivesDay()
        {
            return entities
                .Include(o => o.User)
                .Where(o => o.Finished == false && o.EndingDate <= DateTime.Now.AddDays(1.0));
        }

        public IEnumerable<Objective> IncomingObjectivesWeek()
        {
            return entities
                .Include(o => o.User)
                .Where(o => o.Finished == false && o.EndingDate <= DateTime.Now.AddDays(7.0));
        }

        public async Task<Objective> SingleUsersObjective(long objectiveId, long userId)
        {
            return await entities.SingleOrDefaultAsync(o => o.Id == objectiveId && o.UserId == userId);
        }
    }
}
