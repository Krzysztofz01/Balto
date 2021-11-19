using System.Threading.Tasks;

namespace Balto.Domain.Goals
{
    public interface IGoalBackgroundJob
    {
        Task DailyResetRecurringGoals();
    }
}
