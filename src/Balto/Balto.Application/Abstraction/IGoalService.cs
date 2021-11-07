using Balto.Domain.Goals;
using System.Threading.Tasks;

namespace Balto.Application.Abstraction
{
    public interface IGoalService
    {
        Task Handle(IApplicationCommand<Goal> command);
    }
}
