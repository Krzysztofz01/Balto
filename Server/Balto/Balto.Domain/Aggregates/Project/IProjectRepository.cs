using System.Threading.Tasks;

namespace Balto.Domain.Aggregates.Project
{
    public interface IProjectRepository
    {
        Task<Project> Load(ProjectId id);
        Task<Project> LoadByToken(string ticketToken);
        Task Add(Project entity);
        Task<bool> Exists(ProjectId id);
    }
}
