﻿using Balto.Application.Abstraction;
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
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IdentityService(IIdentityRepository identityRepository, IUnitOfWork unitOfWork)
        {
            _identityRepository = identityRepository ??
                throw new ArgumentNullException(nameof(identityRepository));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(IApplicationCommand<Identity> command)
        {
            switch(command)
            {
                case V1.Delete c: await Apply(c.Id, new IdentityDeleted { Id = c.Id }); break;
                case V1.Update c: await Apply(c.Id, new IdentityUpdated { Id = c.Id, Color = c.Color}); break;
                case V1.Activation c: await Apply(c.Id, new IdentityActivationChanged { Id = c.Id, Activated = c.Activated }); break;
                case V1.RoleChange c: await Apply(c.Id, new IdentityRoleChanged { Id = c.Id, Role = c.Role }); break;
                case V1.TeamChange c: await Apply(c.Id, new IdentityTeamChanged { Id = c.Id, TeamId = c.TeamId }); break;
                
                default: throw new InvalidOperationException("This command is not supported.");
            }
        }

        private async Task Apply(Guid id, IEventBase @event)
        {
            var identity = await _identityRepository.Get(id);

            identity.Apply(@event);

            await _unitOfWork.Commit();
        }
    }
}