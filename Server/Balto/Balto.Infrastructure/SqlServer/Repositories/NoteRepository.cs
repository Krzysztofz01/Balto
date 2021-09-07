using Balto.Domain.Aggregates.Note;
using Balto.Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Balto.Infrastructure.SqlServer.Repositories
{
    public class NoteRepository : INoteRepository, IDisposable
    {
        private readonly BaltoDbContext _dbContext;

        public NoteRepository(BaltoDbContext dbContext)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task Add(Note entity)
        {
            await _dbContext.Notes.AddAsync(entity);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<bool> Exists(NoteId id)
        {
            return await _dbContext.Notes.AnyAsync(e => e.Id.Value == id.Value);
        }

        public async Task<Note> Load(NoteId id)
        {
            return await _dbContext.Notes.SingleAsync(e => e.Id.Value == id.Value);
        }
    }
}
