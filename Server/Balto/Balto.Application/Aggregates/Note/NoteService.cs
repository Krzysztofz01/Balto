using Balto.Domain.Aggregates.Note;
using Balto.Domain.Common;
using Balto.Infrastructure.Abstraction;
using System;
using System.Threading.Tasks;
using static Balto.Application.Aggregates.Note.Commands;

namespace Balto.Application.Aggregates.Note
{
    public class NoteService : IApplicationService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestAuthorizationHandler _requestAuthorizationHandler;

        public NoteService(
            INoteRepository noteRepository,
            IUnitOfWork unitOfWork,
            IRequestAuthorizationHandler requestAuthorizationHandler)
        {
            _noteRepository = noteRepository ??
                throw new ArgumentNullException(nameof(noteRepository));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _requestAuthorizationHandler = requestAuthorizationHandler ??
                throw new ArgumentNullException(nameof(requestAuthorizationHandler));
        }

        public async Task Handle(object command)
        {
            switch(command)
            {
                case V1.NoteAdd cmd:
                    await HandleCreateV1(cmd);
                    break;

                case V1.NoteDelete cmd:
                    await HandleUpdate(cmd.Id, c => c.Delete(_requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.NoteUpdate cmd:
                    await HandleUpdate(cmd.Id, c => c.Update(cmd.Title, cmd.Content));
                    break;

                case V1.NoteChangePublication cmd:
                    await HandleUpdate(cmd.Id, c => c.ChangePublication(_requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.NoteAddContributor cmd:
                    await HandleUpdate(cmd.Id, c => c.AddContributor(cmd.UserId, _requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.NoteDeleteContributor cmd:
                    await HandleUpdate(cmd.Id, c => c.DeleteContributor(cmd.UserId, _requestAuthorizationHandler.GetUserGuid()));
                    break;

                case V1.NoteLeave cmd:
                    await HandleUpdate(cmd.Id, c => c.Leave(_requestAuthorizationHandler.GetUserGuid()));
                    break;
            }
        }

        private async Task HandleUpdate(Guid noteId, Action<Domain.Aggregates.Note.Note> operation)
        {
            var note = await _noteRepository.Load(noteId.ToString());
            if (note is null) throw new InvalidOperationException($"Note with given id: { noteId } not found.");

            operation(note);

            await _unitOfWork.Commit();
        }

        private async Task HandleCreateV1(V1.NoteAdd cmd)
        {
            var note = Domain.Aggregates.Note.Note.Factory.Create(
                new NoteOwnerId(_requestAuthorizationHandler.GetUserGuid()),
                NoteTitle.FromString(cmd.Title),
                NoteContent.FromString(cmd.Content));

            await _noteRepository.Add(note);

            await _unitOfWork.Commit();
        }
    }
}
