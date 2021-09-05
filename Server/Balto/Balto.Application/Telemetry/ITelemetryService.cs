using System;
using System.Threading.Tasks;

namespace Balto.Application.Telemetry
{
    public interface ITelemetryService
    {
        Task LogException(Exception exception, string message = null);
        Task Ping();
    }
}
