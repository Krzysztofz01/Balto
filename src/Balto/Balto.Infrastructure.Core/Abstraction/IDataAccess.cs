using Balto.Domain.Goals;
using Balto.Domain.Identities;
using Balto.Domain.Projects;
using System.Linq;

namespace Balto.Infrastructure.Core.Abstraction
{
    public interface IDataAccess
    {
        IQueryable<Identity> Identities { get; }
        IQueryable<Project> Projects { get; }
        IQueryable<Goal> Goals { get; }
    }
}
