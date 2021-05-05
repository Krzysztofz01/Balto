using Balto.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public interface IObjectiveRepository : IRepository<Objective>
    {
        IEnumerable<Objective> AllUsersObjectives(long userId);
        Task<Objective> SingleUsersObjective(long objectiveId, long userId);
        IEnumerable<Objective> IncomingObjectivesDay();
        IEnumerable<Objective> IncomingObjectivesWeek();
    }
}
