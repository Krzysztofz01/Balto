using Balto.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public interface IProjectTableEntryRepository : IRepository<ProjectTableEntry>
    {
        IEnumerable<ProjectTableEntry> AllUsersEntries(long projectId, long projectTableId, long userId);
        Task<ProjectTableEntry> SingleUsersEntry(long projectId, long projectTableId, long projectTableEntryId, long userId);
        Task<long> GetEntryOrder(long projectTableId);
    }
}
