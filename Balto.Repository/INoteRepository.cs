using Balto.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public interface INoteRepository : IRepository<Note>
    {
        IEnumerable<Note> AllUsersNotes(long userId);
        Task<Note> SingleUsersNote(long noteId, long userId);
    }
}
