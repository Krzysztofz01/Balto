using Balto.Domain.Identities;
using System.Threading.Tasks;

namespace Balto.Application.Abstraction
{
    public interface IIdentityService
    {
        Task Handle(IApplicationCommand<Identity> command);
    }
}
