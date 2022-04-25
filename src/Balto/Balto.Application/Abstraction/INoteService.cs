using Balto.Domain.Notes;
using System.Threading.Tasks;

namespace Balto.Application.Abstraction
{
    public interface INoteService
    {
        Task Handle(IApplicationCommand<Note> command);
    }
}
