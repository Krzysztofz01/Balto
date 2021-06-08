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
    public class NoteService : INoteService
    {
        private readonly INoteRepository noteRepository;
        private readonly INoteReadWriteUserRepository noteReadWriteUserRepository;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public NoteService(
            INoteRepository noteRepository,
            INoteReadWriteUserRepository noteReadWriteUserRepository,
            IUserService userService,
            IMapper mapper)
        {
            this.noteRepository = noteRepository ??
                throw new ArgumentNullException(nameof(noteRepository));

            this.noteReadWriteUserRepository = noteReadWriteUserRepository ??
                throw new ArgumentNullException(nameof(NoteReadWriteUserRepository));

            this.userService = userService ??
                throw new ArgumentNullException(nameof(userService));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IServiceResult> Add(NoteDto note, long userId)
        {
            var noteMapped = mapper.Map<Note>(note);
            noteMapped.OwnerId = userId;

            await noteRepository.Add(noteMapped);
            if (await noteRepository.Save() > 0) return new ServiceResult(ResultStatus.Sucess);
            return new ServiceResult(ResultStatus.Failed);
        }

        public async Task<IServiceResult> Delete(long noteId, long userId)
        {
            var note = await noteRepository.SingleUsersNoteOwner(noteId, userId);
            if (note is null) return new ServiceResult(ResultStatus.NotFound);

            noteRepository.Remove(note);
            if (await noteRepository.Save() > 0) return new ServiceResult(ResultStatus.Sucess);
            return new ServiceResult(ResultStatus.Failed);
        }

        public async Task<ServiceResult<NoteDto>> Get(long noteId, long userId)
        {
            var note = await noteRepository.SingleUsersNote(noteId, userId);
            if (note is null) return new ServiceResult<NoteDto>(ResultStatus.NotFound);

            return new ServiceResult<NoteDto>(mapper.Map<NoteDto>(note));
        }

        public async Task<ServiceResult<IEnumerable<NoteDto>>> GetAll(long userId)
        {
            var notes = noteRepository.AllUsersNotes(userId);

            return new ServiceResult<IEnumerable<NoteDto>>(mapper.Map<IEnumerable<NoteDto>>(notes));
        }

        public async Task<IServiceResult> InviteUser(long noteId, string collaboratorEmail, long userId)
        {
            //Check if the user who requested the invitation is the owner
            //Only owners have permission to invite collaborators
            if (!await noteRepository.IsOwner(noteId, userId)) return new ServiceResult(ResultStatus.NotPermited);

            var collaboratorId = await userService.GetIdByEmail(collaboratorEmail);
            if (collaboratorId is null) return new ServiceResult(ResultStatus.NotFound);
            
            if (await noteRepository.IsOwner(noteId, (long)collaboratorId)) return new ServiceResult(ResultStatus.NotPermited);

            await noteReadWriteUserRepository.AddCollaborator(noteId, (long)collaboratorId);
            if (await noteReadWriteUserRepository.Save() > 0) return new ServiceResult(ResultStatus.Sucess);
            return new ServiceResult(ResultStatus.Failed);
        }

        public async Task<IServiceResult> Leave(long noteId, long userId)
        {
            var note = await noteRepository.SingleUsersNote(noteId, userId);

            if (note is null) return new ServiceResult(ResultStatus.NotFound);
            if (note.OwnerId == userId) return new ServiceResult(ResultStatus.NotPermited);

            if (await noteReadWriteUserRepository.RemoveCollaborator(noteId, userId))
            {
                if(await noteRepository.Save() > 0)
                {
                    return new ServiceResult(ResultStatus.Sucess);
                }
                return new ServiceResult(ResultStatus.Failed);
            }
            return new ServiceResult(ResultStatus.NotFound);
        }

        public async Task<IServiceResult> Update(NoteDto note, long noteId, long userId)
        {
            //Possible changes: Name and Content
            var noteMatch = await noteRepository.SingleUsersNote(noteId, userId);
            if (noteMatch is null) return new ServiceResult(ResultStatus.NotFound);

            bool changes = false;

            if(noteMatch.Name != note.Name && note.Name != null)
            {
                noteMatch.Name = note.Name;
                changes = true;
            }

            if(noteMatch.Content != note.Content && note.Content != null)
            {
                noteMatch.Content = note.Content;
                changes = true;
            }

            if(changes)
            {
                if (await noteRepository.Save() > 0) return new ServiceResult(ResultStatus.Sucess);
                return new ServiceResult(ResultStatus.Failed);
            }
            return new ServiceResult(ResultStatus.Sucess);
        }
    }
}
