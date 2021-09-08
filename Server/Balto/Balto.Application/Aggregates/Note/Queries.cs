using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Application.Aggregates.Note
{
    public static class Queries
    {
        public static async Task<IEnumerable<Domain.Aggregates.Note.Note>> GetAll(this DbSet<Domain.Aggregates.Note.Note> entities)
        {
            return await entities
                .AsNoTracking()
                .ToListAsync();
        }

        public static async Task<Domain.Aggregates.Note.Note> GetById(this DbSet<Domain.Aggregates.Note.Note> entities, Guid noteId)
        {
            return await entities
                .AsNoTracking()
                .SingleAsync(e => e.Id.Value == noteId);
        }

        public static async Task<IEnumerable<Domain.Aggregates.Note.Note>> GetAllPublic(this DbSet<Domain.Aggregates.Note.Note> entities)
        {
            return await entities
                .AsNoTracking()
                .Where(e => e.Public)
                .ToListAsync();
        }

        public static async Task<Domain.Aggregates.Note.Note> GetByIdPublic(this DbSet<Domain.Aggregates.Note.Note> entities, Guid noteId)
        {
            return await entities
                .AsNoTracking()
                .SingleAsync(e => e.Id.Value == noteId && e.Public);
        }
    }
}
