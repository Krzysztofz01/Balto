using System;
using System.Threading.Tasks;

namespace Balto.Domain.Projects
{
    public interface IProjectRepository
    {
        Task Add(Project project);
        Task<Project> Get(Guid id);
        Task<bool> Exists(Guid id);
    }
}
