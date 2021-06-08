using Balto.Domain;
using Balto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public class NoteReadWriteUserRepository : Repository<NoteReadWriteUser>, INoteReadWriteUserRepository
    {
        public NoteReadWriteUserRepository(BaltoDbContext context) : base(context)
        {
        }

        public async Task AddCollaborator(long noteId, long collaboratorId)
        {
            if(!await entities.AnyAsync(o => o.NoteId == noteId && o.UserId == collaboratorId))
            {
                entities.Add(new NoteReadWriteUser
                {
                    NoteId = noteId,
                    UserId = collaboratorId
                });
            }
        }

        public IEnumerable<long?> GetNotesIds(long userId)
        {
            return entities.Where(o => o.UserId == userId).Select(o => o.NoteId);
        }

        public IEnumerable<long?> GetUsersIds(long noteId)
        {
            return entities.Where(o => o.NoteId == noteId).Select(o => o.UserId);
        }

        public async Task<bool> IsRelated(long noteId, long userId)
        {
            if (await entities.AnyAsync(o => o.NoteId == noteId && o.UserId == userId)) return true;
            return false;
        }

        public async Task<bool> RemoveCollaborator(long noteId, long collaboratorId)
        {
            var relation = await entities.SingleOrDefaultAsync(n => n.NoteId == noteId && n.UserId == collaboratorId);
            if (relation is null) return false;

            entities.Remove(relation);
            return true;
        }
    }
}
