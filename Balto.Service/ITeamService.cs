using Balto.Domain;
using Balto.Service.Dto;
using Balto.Service.Handlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface ITeamService
    {
        Task<ServiceResult<IEnumerable<Team>>> GetAll(long leaderUserId);
        Task<ServiceResult<Team>> Get(long teamId, long leaderUserId);
        Task<IServiceResult> Add(TeamDto team, long leaderUserId);
        Task<IServiceResult> Delete(long teamId, long leaderUserId);
        Task<IServiceResult> Update(TeamDto team, long teamId, long leaderUserId);
        Task<ServiceResult<Team>> GetUsersTeam(long userId);
    }
}
