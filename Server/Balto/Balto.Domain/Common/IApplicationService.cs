using System.Threading.Tasks;

namespace Balto.Domain.Common
{
    public interface IApplicationService
    {
        Task Handle(object command);
    }
}
