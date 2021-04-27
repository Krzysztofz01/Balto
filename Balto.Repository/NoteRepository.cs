using Balto.Domain;
using Balto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        public NoteRepository(BaltoDbContext context) : base(context)
        {
        }

        public IEnumerable<Note> AllUsersNotes(long userId)
        {
            return entities
                .Include(n => n.Owner)
                .Include(n => n.ReadWriteUsers).ThenInclude(n => n.User)
                .Where(n => n.OwnerId == userId || n.ReadWriteUsers.Any(u => u.UserId == userId));
        }

        public async Task<bool> IsOwner(long noteId, long userId)
        {
            return await entities
                .AnyAsync(n => n.Id == noteId && n.OwnerId == userId);
        }

        public async Task<Note> SingleUsersNote(long noteId, long userId)
        {
            return await entities
                .Include(n => n.Owner)
                .Include(n => n.ReadWriteUsers).ThenInclude(n => n.User)
                .Where(n => n.OwnerId == userId || n.ReadWriteUsers.Any(u => u.UserId == userId))
                .SingleOrDefaultAsync(n => n.Id == noteId);  
        }

        public async Task<Note> SingleUsersNoteOwner(long noteId, long userId)
        {
            return await entities
                .Include(n => n.Owner)
                .Include(n => n.ReadWriteUsers).ThenInclude(n => n.User)
                .Where(n => n.OwnerId == userId)
                .SingleOrDefaultAsync(n => n.Id == noteId);
        }
    }
}
