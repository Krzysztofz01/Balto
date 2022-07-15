using Balto.Application.Abstraction;
using Balto.Domain.Core.Events;
using Balto.Domain.Team;
using Balto.Infrastructure.Core.Abstraction;
using System;
using System.Threading.Tasks;
using static Balto.Application.Teams.Commands;
using static Balto.Domain.Team.Events.V1;

namespace Balto.Application.Teams
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(IApplicationCommand<Team> command)
        {
            switch (command)
            {
                case V1.Create c: await Create(new TeamCreated { Name = c.Name, Color = c.Color }); break;
                case V1.Delete c: await Apply(c.Id, new TeamDeleted { Id = c.Id }); break;

                case V1.AddMember c: await Apply(c.Id, new TeamMemberAdded { Id = c.Id, IdentityId = c.UserId }); break;
                case V1.DeleteMember c: await Apply(c.Id, new TeamMemberDeleted { Id = c.Id, IdentityId = c.UserId }); break;

                default: throw new InvalidOperationException("This command is not supported.");
            }
        }

        private async Task Apply(Guid id, IEventBase @event)
        {
            var team = await _unitOfWork.TeamRepository.Get(id);

            team.Apply(@event);

            await _unitOfWork.Commit();
        }

        private async Task Create(TeamCreated @event)
        {
            var team = Team.Factory.Create(@event);

            await _unitOfWork.TeamRepository.Add(team);

            await _unitOfWork.Commit();
        }
    }
}
