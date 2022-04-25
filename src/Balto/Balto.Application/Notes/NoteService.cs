using Balto.Application.Abstraction;
using Balto.Domain.Core.Events;
using Balto.Domain.Notes;
using Balto.Infrastructure.Core.Abstraction;
using System;
using System.Threading.Tasks;
using static Balto.Application.Notes.Commands;
using static Balto.Domain.Notes.Events.V1;

namespace Balto.Application.Notes
{
    public class NoteService : INoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScopeWrapperService _scopeWrapperService;

        private Guid UserId => _scopeWrapperService.GetUserId();

        public NoteService(IUnitOfWork unitOfWork, IScopeWrapperService scopeWrapperService)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _scopeWrapperService = scopeWrapperService ??
                throw new ArgumentNullException(nameof(scopeWrapperService));
        }

        public async Task Handle(IApplicationCommand<Note> command)
        {
            switch (command)
            {
                case V1.Create c: await Create(new NoteCreated { Title = c.Title, CurrentUserId = UserId }); break;

                case V1.Delete c: await Apply(c.Id, new NoteDeleted { Id = c.Id, CurrentUserId = UserId }); break;
                case V1.Update c: await Apply(c.Id, new NoteUpdated { Id = c.Id, Title = c.Title, Content = c.Content, CurrentUserId = UserId }); break;
                case V1.AddContributor c: await Apply(c.Id, new NoteContributorAdded { Id = c.Id, UserId = c.UserId, CurrentUserId = UserId }); break;
                case V1.DeleteContributor c: await Apply(c.Id, new NoteContributorDeleted { Id = c.Id, UserId = c.UserId, CurrentUserId = UserId }); break;
                case V1.UpdateContributor c: await Apply(c.Id, new NoteContributorUpdated { Id = c.Id, UserId = c.UserId, Role = c.Role, CurrentUserId = UserId }); break;
                case V1.LeaveAsContributor c: await Apply(c.Id, new NoteContributorLeft { Id = c.Id, CurrentUserId = UserId }); break;
                case V1.TagAssign c: await Apply(c.Id, new NoteTagAssigned { Id = c.Id, TagId = c.TagId, CurrentUserId = UserId }); break;
                case V1.TagUnassign c: await Apply(c.Id, new NoteTagUnassigned { Id = c.Id, TagId = c.TagId, CurrentUserId = UserId }); break;
                case V1.SnapshotCreate c: await Apply(c.Id, new NoteSnapshotCreated { }); break;
                case V1.SnapshotDelete c: await Apply(c.Id, new NoteSnapshotDeleted { Id = c.Id, SnapshotId = c.SnapshotId, CurrentUserId = UserId }); break;

                default: throw new InvalidOperationException("This command is not supported.");
            }
        }

        private async Task Apply(Guid id, IEventBase @event)
        {
            var goal = await _unitOfWork.NoteRepository.Get(id);

            goal.Apply(@event);

            await _unitOfWork.Commit();
        }

        private async Task Apply(Guid id, NoteSnapshotCreated _)
        {
            var goal = await _unitOfWork.NoteRepository.Get(id);

            goal.Apply(new NoteSnapshotCreated
            {
                Id = goal.Id,
                Content = goal.Content,
                CurrentUserId = UserId
            });

            await _unitOfWork.Commit();
        }

        private async Task Create(NoteCreated @event)
        {
            var note = Note.Factory.Create(@event);

            await _unitOfWork.NoteRepository.Add(note);

            await _unitOfWork.Commit();
        }
    }
}
