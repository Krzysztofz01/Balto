using Balto.Domain.Goals;
using Balto.Domain.Identities;
using Balto.Domain.Projects;
using Balto.Domain.Tags;
using System.Threading.Tasks;

namespace Balto.Infrastructure.Core.Abstraction
{
    public interface IUnitOfWork
    {
        IIdentityRepository IdentityRepository { get; }
        IGoalRepository GoalRepository { get; }
        IProjectRepository ProjectRepository { get; }
        ITagRepository TagRepository { get; }

        Task Commit();
    }
}
