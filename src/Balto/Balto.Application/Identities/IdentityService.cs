using Balto.Application.Abstraction;
using Balto.Application.Logging;
using Balto.Domain.Core.Events;
using Balto.Domain.Identities;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Balto.Application.Identities.Commands;
using static Balto.Domain.Identities.Events.V1;

namespace Balto.Application.Identities
{
    public class IdentityService : IIdentityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(IUnitOfWork unitOfWork, ILogger<IdentityService> logger)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(IApplicationCommand<Identity> command)
        {
            _logger.LogApplicationCommand(command);

            switch(command)
            {
                case V1.Delete c: await Apply(c.Id, new IdentityDeleted { Id = c.Id }); break;
                case V1.Update c: await Apply(c.Id, new IdentityUpdated { Id = c.Id, Color = c.Color}); break;
                case V1.Activation c: await Apply(c.Id, new IdentityActivationChanged { Id = c.Id, Activated = c.Activated.Value }); break;
                case V1.RoleChange c: await Apply(c.Id, new IdentityRoleChanged { Id = c.Id, Role = c.Role.Value }); break;
                
                default: throw new InvalidOperationException("This command is not supported.");
            }
        }

        private async Task Apply(Guid id, IEventBase @event)
        {
            var identity = await _unitOfWork.IdentityRepository.Get(id);

            _logger.LogDomainEvent(@event);

            identity.Apply(@event);

            await _unitOfWork.Commit();
        }
    }
}
