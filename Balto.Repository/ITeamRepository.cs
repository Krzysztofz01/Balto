using Balto.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<Team> GetUsersTeam(long userId);
        IEnumerable<Team> AllTeams();
        Task<Team> SingleTeam(long teamId);
    }
}
