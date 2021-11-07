using Balto.Domain.Core.Model;

namespace Balto.Application.Abstraction
{
    public interface IApplicationCommand<TAggreagreRoot> where TAggreagreRoot : AggregateRoot
    {
    }
}
