using System;
using System.Threading.Tasks;

namespace Balto.Domain.Goals
{
    public interface IGoalRepository
    {
        Task Add(Goal goal);
        Task<Goal> Get(Guid id);
        Task<bool> Exists(Guid id);
    }
}
