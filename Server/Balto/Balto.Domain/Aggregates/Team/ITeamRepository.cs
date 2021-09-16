using System.Threading.Tasks;

namespace Balto.Domain.Aggregates.Team
{
    public interface ITeamRepository
    {
        Task<Team> Load(TeamId id);
        Task Add(Team entity);
        Task<bool> Exists(TeamId id);
    }
}
