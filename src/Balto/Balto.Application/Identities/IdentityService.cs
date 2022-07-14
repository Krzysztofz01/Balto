using Balto.Application.Abstraction;
using Balto.Domain.Core.Events;
using Balto.Domain.Identities;
using Balto.Infrastructure.Core.Abstraction;
using System;
using System.Threading.Tasks;
using static Balto.Application.Identities.Commands;
using static Balto.Domain.Identities.Events.V1;

namespace Balto.Application.Identities
{
    public class IdentityService : IIdentityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IdentityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(IApplicationCommand<Identity> command)
        {
            switch(command)
            {
                case V1.Delete c: await Apply(c.Id, new IdentityDeleted { Id = c.Id }); break;
                case V1.Update c: await Apply(c.Id, new IdentityUpdated { Id = c.Id, Color = c.Color}); break;
                case V1.Activation c: await Apply(c.Id, new IdentityActivationChanged { Id = c.Id, Activated = c.Activated.Value }); break;
                case V1.RoleChange c: await Apply(c.Id, new IdentityRoleChanged { Id = c.Id, Role = c.Role.Value }); break;
                case V1.TeamChange c: await Apply(c.Id, new IdentityTeamChanged { Id = c.Id, TeamId = c.TeamId }); break;
                
                default: throw new InvalidOperationException("This command is not supported.");
            }
        }

        private async Task Apply(Guid id, IEventBase @event)
        {
            var identity = await _unitOfWork.IdentityRepository.Get(id);

            identity.Apply(@event);

            await _unitOfWork.Commit();
        }
    }
}
