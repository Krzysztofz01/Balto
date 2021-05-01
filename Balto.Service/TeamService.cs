using AutoMapper;
using Balto.Domain;
using Balto.Repository;
using Balto.Service.Dto;
using Balto.Service.Handlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public class TeamService : ITeamService
    {
        private readonly IUserRepository userRepository;
        private readonly ITeamRepository teamRepository;
        private readonly IMapper mapper;

        public TeamService(
            IUserRepository userRepository,
            ITeamRepository teamRepository,
            IMapper mapper)
        {
            this.userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));

            this.teamRepository = teamRepository ??
                throw new ArgumentNullException(nameof(teamRepository));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IServiceResult> Add(TeamDto team, long leaderUserId)
        {
            if (!await userRepository.IsLeader(leaderUserId)) return new ServiceResult(ResultStatus.NotPermited);

            var teamMapped = mapper.Map<Team>(team);
            await teamRepository.Add(teamMapped);

            if (await teamRepository.Save() > 0) return new ServiceResult(ResultStatus.Sucess);
            return new ServiceResult(ResultStatus.Sucess);
        }

        public async Task<IServiceResult> Delete(long teamId, long leaderUserId)
        {
            if (!await userRepository.IsLeader(leaderUserId)) return new ServiceResult(ResultStatus.NotPermited);

            var team = await teamRepository.SingleTeam(teamId);
            if (team is null) return new ServiceResult(ResultStatus.NotFound);

            teamRepository.Remove(team);
            if (await teamRepository.Save() > 0) return new ServiceResult(ResultStatus.Sucess);
            return new ServiceResult(ResultStatus.Failed);
        }

        public async Task<ServiceResult<TeamDto>> Get(long teamId, long leaderUserId)
        {
            if (!await userRepository.IsLeader(leaderUserId)) return new ServiceResult<TeamDto>(ResultStatus.NotPermited);

            var team = await teamRepository.SingleTeam(teamId);
            if (team is null) return new ServiceResult<TeamDto>(ResultStatus.NotFound);

            var teamMapped = mapper.Map<TeamDto>(team);
            return new ServiceResult<TeamDto>(teamMapped);
        }

        public async Task<ServiceResult<IEnumerable<TeamDto>>> GetAll(long leaderUserId)
        {
            if (!await userRepository.IsLeader(leaderUserId)) return new ServiceResult<IEnumerable<TeamDto>>(ResultStatus.NotPermited);

            var teams = teamRepository.AllTeams();
            var teamsMapped = mapper.Map<IEnumerable<TeamDto>>(teams);
            return new ServiceResult<IEnumerable<TeamDto>>(teamsMapped);
        }

        public async Task<ServiceResult<TeamDto>> GetUsersTeam(long userId)
        {
            var team = await teamRepository.GetUsersTeam(userId);
            if (team is null) return new ServiceResult<TeamDto>(ResultStatus.NotFound);

            var teamMapped = mapper.Map<TeamDto>(team);
            return new ServiceResult<TeamDto>(teamMapped);
        }

        public async Task<IServiceResult> Update(TeamDto team, long teamId, long leaderUserId)
        {
            //Possible changes: name
            if (!await userRepository.IsLeader(leaderUserId)) return new ServiceResult(ResultStatus.NotPermited);

            var teamBase = await teamRepository.SingleTeam(teamId);
            if (teamBase is null) return new ServiceResult(ResultStatus.NotFound);

            bool changes = false;

            if(teamBase.Name != team.Name && team.Name != null)
            {
                changes = true;
                teamBase.Name = team.Name;
            }

            if(changes)
            {
                if(await teamRepository.Save() > 0) return new ServiceResult(ResultStatus.Sucess);
                return new ServiceResult(ResultStatus.Failed);
            }
            return new ServiceResult(ResultStatus.Sucess);
        }
    }
}
