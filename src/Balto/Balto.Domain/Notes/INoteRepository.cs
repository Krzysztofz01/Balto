using System;
using System.Threading.Tasks;

namespace Balto.Domain.Notes
{
    public interface INoteRepository
    {
        Task Add(Note note);
        Task<Note> Get(Guid id);
        Task<bool> Exists(Guid id);
    }
}
