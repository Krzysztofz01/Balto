using System;
using System.Threading.Tasks;

namespace Balto.Domain.Projects
{
    public interface IProjectRepository
    {
        Task Add(Project project);
        Task<Project> Get(Guid id);
        Task<Project> Get(string ticketToken);
        Task<bool> Exists(Guid id);
    }
}
