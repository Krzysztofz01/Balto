using Balto.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public interface INoteReadWriteUserRepository : IRepository<NoteReadWriteUser>
    {
        IEnumerable<long?> GetUsersIds(long noteId);
        IEnumerable<long?> GetNotesIds(long userId);
        Task<bool> IsRelated(long noteId, long userId);
        Task AddCollaborator(long noteId, long collaboratorId);
    }
}
