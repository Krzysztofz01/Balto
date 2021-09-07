using System.Threading.Tasks;

namespace Balto.Domain.Aggregates.Note
{
    public interface INoteRepository
    {
        Task<Note> Load(NoteId id);
        Task Add(Note entity);
        Task<bool> Exists(NoteId id);
    }
}
