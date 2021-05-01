using Balto.Service.Dto;
using Balto.Service.Handlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface ITeamService
    {
        Task<ServiceResult<IEnumerable<TeamDto>>> GetAll(long leaderUserId);
        Task<ServiceResult<TeamDto>> Get(long teamId, long leaderUserId);
        Task<IServiceResult> Add(TeamDto team, long leaderUserId);
        Task<IServiceResult> Delete(long teamId, long leaderUserId);
        Task<IServiceResult> Update(TeamDto team, long teamId, long leaderUserId);
        Task<ServiceResult<TeamDto>> GetUsersTeam(long userId);
    }
}
