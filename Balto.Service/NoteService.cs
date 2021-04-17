﻿using AutoMapper;
using Balto.Domain;
using Balto.Repository;
using Balto.Service.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository noteRepository;
        private readonly IMapper mapper;

        public NoteService(
            INoteRepository noteRepository,
            IMapper mapper)
        {
            this.noteRepository = noteRepository ??
                throw new ArgumentNullException(nameof(noteRepository));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Add(NoteDto note)
        {
            var noteMapped = mapper.Map<Note>(note);

            await noteRepository.Add(noteMapped);
            if (await noteRepository.Save() > 0) return true;
            return false;
        }

        public async Task<bool> Delete(long noteId, long userId)
        {
            var note = await noteRepository.SingleOrDefault(n => n.OwnerId == userId && n.Id == noteId);
            if (note is null) return false;


            noteRepository.Remove(note);
            if (await noteRepository.Save() > 0) return true;
            return false;
        }

        public async Task<NoteDto> Get(long noteId, long userId)
        {
            var note = await noteRepository.SingleOrDefault(n => n.OwnerId == userId && n.Id == noteId);

            return mapper.Map<NoteDto>(note);
        }

        public async Task<IEnumerable<NoteDto>> GetAll(long userId)
        {
            var notes = noteRepository.Find(n => n.OwnerId == userId);

            return mapper.Map<IEnumerable<NoteDto>>(notes);
        }

        public async Task<bool> Update(NoteDto note, long userId)
        {
            //Possible changes: Name and Content
            var noteMatch = await noteRepository.SingleOrDefault(p => p.OwnerId == userId && p.Id == note.Id);
            if (noteMatch is null) return false;

            bool changes = false;

            if(noteMatch.Name != note.Name)
            {
                noteMatch.Name = note.Name;
                changes = true;
            }

            if(noteMatch.Content != note.Content)
            {
                noteMatch.Content = note.Content;
                changes = true;
            }

            if(changes)
            {
                if (await noteRepository.Save() > 0) return true;
            }
            return false;
        }
    }
}
