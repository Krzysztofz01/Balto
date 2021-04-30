using Balto.Domain;
using Balto.Service.Dto;
using Balto.Service.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Balto.Service
{
    public class TeamService : ITeamService
    {
        
        public TeamService()
        {

        }

        public Task<IServiceResult> Add(TeamDto team, long leaderUserId)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult> Delete(long teamId, long leaderUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<Team>> Get(long teamId, long leaderUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<Team>>> GetAll(long leaderUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<Team>> GetUsersTeam(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult> Update(TeamDto team, long teamId, long leaderUserId)
        {
            throw new NotImplementedException();
        }
    }
}
