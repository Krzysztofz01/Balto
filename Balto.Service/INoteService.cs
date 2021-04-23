using Balto.Service.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDto>> GetAll(long userId);
        Task<NoteDto> Get(long noteId, long userId);
        Task<bool> Add(NoteDto note, long userId);
        Task<bool> Delete(long noteId, long userId);
        Task<bool> Update(NoteDto note, long userId);
    }
}
