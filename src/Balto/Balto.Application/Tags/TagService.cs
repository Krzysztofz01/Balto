using Balto.Application.Abstraction;
using Balto.Domain.Core.Events;
using Balto.Domain.Tags;
using Balto.Infrastructure.Core.Abstraction;
using System;
using System.Threading.Tasks;
using static Balto.Application.Tags.Commands;
using static Balto.Domain.Tags.Events.V1;

namespace Balto.Application.Tags
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(IApplicationCommand<Tag> command)
        {
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

            tag.Apply(@event);

            await _unitOfWork.Commit();
        }

        private async Task Create(TagCreated @event)
        {
            var tag = Tag.Factory.Create(@event);

            await _unitOfWork.TagRepository.Add(tag);

            await _unitOfWork.Commit();
        }
    }
}
