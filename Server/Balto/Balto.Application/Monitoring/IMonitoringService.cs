using System;
using System.Threading.Tasks;

namespace Balto.Application.Monitoring
{
    public interface IMonitoringService
    {
        Task Ping(bool startup = false);
        Task ReportException(Exception e, string message = null);
        Task ReportInstanceStatus();
    }
}
