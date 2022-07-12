using System.Threading.Tasks;

namespace Balto.Application.Plugin.Core
{
    public interface IBaltoIntegration
    {
        Task Handle(IPluginCommand command);
    }
}
