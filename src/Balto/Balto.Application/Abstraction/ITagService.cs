using Balto.Domain.Tags;
using System.Threading.Tasks;

namespace Balto.Application.Abstraction
{
    public interface ITagService
    {
        Task Handle(IApplicationCommand<Tag> command);
    }
}
