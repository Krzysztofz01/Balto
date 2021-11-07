using Balto.Domain.Projects;
using System.Threading.Tasks;

namespace Balto.Application.Abstraction
{
    public interface IProjectService
    {
        Task Handle(IApplicationCommand<Project> command);
    }
}
