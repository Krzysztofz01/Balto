using System.Threading.Tasks;

namespace Balto.Domain.Aggregates.Objective
{
    public interface IObjectiveRepository
    {
        Task<Objective> Load(ObjectiveId id);
        Task Add(Objective entity);
        Task<bool> Exists(ObjectiveId id);
    }
}
