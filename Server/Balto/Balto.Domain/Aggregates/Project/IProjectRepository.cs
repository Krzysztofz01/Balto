using System.Threading.Tasks;

namespace Balto.Domain.Aggregates.Project
{
    public interface IProjectRepository
    {
        Task<Project> Load(ProjectId id);
        Task Add(Project entity);
        Task<bool> Exists(ProjectId id);
    }
}
