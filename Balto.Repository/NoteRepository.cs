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
            return entities.Where(n => n.OwnerId == userId);
        }

        public async Task<Note> SingleUsersNote(long noteId, long userId)
        {
            return await entities.SingleOrDefaultAsync(n => n.Id == noteId && n.OwnerId == userId);
        }
    }
}
