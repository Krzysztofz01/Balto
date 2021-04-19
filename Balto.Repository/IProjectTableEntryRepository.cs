using Balto.Domain;

namespace Balto.Repository
{
    public interface IProjectTableEntryRepository : IRepository<ProjectTableEntry>
    {
        long GetEntryOrder(long projectTableId);
    }
}
