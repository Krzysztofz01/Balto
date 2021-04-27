using Balto.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public interface IProjectTableRepository : IRepository<ProjectTable>
    {
        IEnumerable<ProjectTable> AllUserTabels(long projectId, long userId);
        Task<ProjectTable> SingleUsersTable(long projectId, long tableId, long userId);
        Task<ProjectTable> SingleUsersTableOwner(long projectId, long tableId, long userId);
    }
}
