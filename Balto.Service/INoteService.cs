using Balto.Service.Dto;
using Balto.Service.Handlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public interface INoteService
    {
        Task<ServiceResult<IEnumerable<NoteDto>>> GetAll(long userId);
        Task<ServiceResult<NoteDto>> Get(long noteId, long userId);
        Task<IServiceResult> Add(NoteDto note, long userId);
        Task<IServiceResult> Delete(long noteId, long userId);
        Task<IServiceResult> Update(NoteDto note, long noteId, long userId);
        Task<IServiceResult> InviteUser(long noteId, string collaboratorEmail, long userId);
        Task<IServiceResult> Leave(long noteId, long userId);
    }
}
