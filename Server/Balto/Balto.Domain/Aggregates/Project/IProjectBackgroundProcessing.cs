using System.Threading.Tasks;

namespace Balto.Domain.Aggregates.Project
{
    public interface IProjectBackgroundProcessing
    {
        Task SendEmailNotifications();
    }
}
