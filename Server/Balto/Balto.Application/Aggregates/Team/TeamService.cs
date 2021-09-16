using Balto.Domain.Aggregates.Team;
using Balto.Domain.Common;
using System;
using System.Threading.Tasks;
using static Balto.Application.Aggregates.Team.Commands;

namespace Balto.Application.Aggregates.Team
{
    public class TeamService : IApplicationService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TeamService(
            ITeamRepository teamRepository,
            IUnitOfWork unitOfWork)
        {
            _teamRepository = teamRepository ??
                throw new ArgumentNullException(nameof(teamRepository));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(object command)
        {
            switch(command)
            {
                case V1.TeamCreate e:
                    await HandleCreateV1(e);
                    break;

                case V1.TeamUpdate e:
                    await HandleUpdate(e.Id, c => c.Update(e.Name, e.Color));
                    break;

                case V1.TeamDelete e:
                    await HandleUpdate(e.Id, c => c.Delete());
                    break;
            }
        }

        private async Task HandleUpdate(Guid teamId, Action<Domain.Aggregates.Team.Team> operation)
        {
            var user = await _teamRepository.Load(teamId.ToString());
            if (user is null) throw new InvalidOperationException($"Team with given id: { teamId } not found.");

            operation(user);

            await _unitOfWork.Commit();
        }

        private async Task HandleCreateV1(V1.TeamCreate cmd)
        {
            var team = Domain.Aggregates.Team.Team.Factory.Create(
                TeamName.FromString(cmd.Name),
                TeamColor.Set(cmd.Color));

            await _teamRepository.Add(team);

            await _unitOfWork.Commit();
        }
    }
}
