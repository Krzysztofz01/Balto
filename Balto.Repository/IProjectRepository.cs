using Balto.Domain;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<Project> SingleUsersProject(long projectId, long userId);
    }
}
