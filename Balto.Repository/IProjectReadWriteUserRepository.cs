using Balto.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public interface IProjectReadWriteUserRepository : IRepository<ProjectReadWriteUser>
    {
        IEnumerable<long?> GetUsersIds(long projectId);
        IEnumerable<long?> GetProjectsIds(long userId);
        Task<bool> IsRelated(long projectId, long userId);
        Task AddCollaborator(long projectId, long collaboratorId);
    }
}
