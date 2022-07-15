using Balto.Domain.Team;
using System.Threading.Tasks;

namespace Balto.Application.Abstraction
{
    public interface ITeamService
    {
        Task Handle(IApplicationCommand<Team> command);
    }
}
