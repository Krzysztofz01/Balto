using System.Threading.Tasks;

namespace Balto.Service
{
    public interface IEmailService
    {
        Task ObjectiveReminderWeek();
        Task ObjectiveReminderDay();
    }
}
