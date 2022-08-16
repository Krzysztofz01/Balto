using Balto.Application.Abstraction;
using Balto.Application.Logging;
using Balto.Domain.Core.Events;
using Balto.Domain.Tags;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Balto.Application.Tags.Commands;
using static Balto.Domain.Tags.Events.V1;

namespace Balto.Application.Tags
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TagService> _logger;

        public TagService(IUnitOfWork unitOfWork, ILogger<TagService> logger)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(IApplicationCommand<Tag> command)
        {
            _logger.LogApplicationCommand(command);

            switch (command)
            {
                case V1.Create c: await Create(new TagCreated { Title = c.Title, Color = c.Color}); break;
                
                case V1.Delete c: await Apply(c.Id, new TagDeleted { Id = c.Id }); break;

                default: throw new InvalidOperationException("This command is not supported.");
            }
        }

        private async Task Apply(Guid id, IEventBase @event)
        {
            var tag = await _unitOfWork.TagRepository.Get(id);

            _logger.LogDomainEvent(@event);

            tag.Apply(@event);

            await _unitOfWork.Commit();
        }

        private async Task Create(TagCreated @event)
        {
            _logger.LogDomainEvent(@event);

            var tag = Tag.Factory.Create(@event);

            await _unitOfWork.TagRepository.Add(tag);

            await _unitOfWork.Commit();
        }
    }
}
