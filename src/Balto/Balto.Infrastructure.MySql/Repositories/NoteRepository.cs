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

        public async Task Add(Note note)
        {
            _ = await _context.Notes.AddAsync(note);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Notes.AnyAsync(n => n.Id == id);
        }

        public async Task<Note> Get(Guid id)
        {
            return await _context.Notes.SingleAsync(n => n.Id == id);
        }
    }
}
