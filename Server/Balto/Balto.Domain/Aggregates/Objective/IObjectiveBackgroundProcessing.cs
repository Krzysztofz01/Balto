using System.Threading.Tasks;

namespace Balto.Domain.Aggregates.Objective
{
    public interface IObjectiveBackgroundProcessing
    {
        Task ResetDailyObjectives();
        Task ResetWeeklyObjectives();
    }
}
