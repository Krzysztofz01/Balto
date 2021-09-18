using System.Threading.Tasks;

namespace Balto.Domain.Aggregates.Project
{
    public interface IProjectBackgroundProcessing
    {
        Task SendEmailNotificationsDayBefore();
        Task SendEmailNotificationsThreeDaysBefore();
    }
}
