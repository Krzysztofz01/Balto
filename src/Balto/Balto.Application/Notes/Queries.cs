using Balto.Domain.Notes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Application.Notes
{
    public static class Queries
    {
        public static async Task<IEnumerable<Note>> GetAllNotes(this IQueryable<Note> notes)
        {
            return await notes
                .Include(n => n.Tags)
                .AsNoTracking()
                .ToListAsync();
        }

        public static async Task<Note> GetNoteById(this IQueryable<Note> notes, Guid noteId)
        {
            return await notes
                .Include(n => n.Tags)
                .Include(n => n.Contributors)
                .Include(n => n.Snapshots)
                .AsNoTracking()
                .SingleAsync(n => n.Id == noteId);
        }
    }
}
