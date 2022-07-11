using Balto.Domain.Notes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Balto.Infrastructure.MySql.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly BaltoDbContext _context;

        public NoteRepository(BaltoDbContext dbContext) =>
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public Task Add(Note note)
        {
            _ = _context.Notes.Add(note);
            return Task.CompletedTask;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Notes
                .AsNoTracking()
                .AnyAsync(n => n.Id == id);
        }

        public async Task<Note> Get(Guid id)
        {
            return await _context.Notes
                .Include(n => n.Contributors)
                .Include(n => n.Snapshots)
                .Include(n => n.Tags)
                .FirstAsync(n => n.Id == id);
        }
    }
}
