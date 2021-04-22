using Balto.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public interface IProjectRepository : IRepository<Project>
    {
        IEnumerable<Project> AllUsersProjects(long userId);
        Task<Project> SingleUsersProject(long projectId, long userId);
    }
}
