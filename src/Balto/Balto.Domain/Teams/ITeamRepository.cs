using System;
using System.Threading.Tasks;

namespace Balto.Domain.Teams
{
    public interface ITeamRepository
    {
        Task Add(Team team);
        Task<Team> Get(Guid id);
        Task<bool> Exists(Guid id);
    }
}
