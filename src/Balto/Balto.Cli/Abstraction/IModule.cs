using System.Threading.Tasks;

namespace Balto.Cli.Abstraction
{
    public interface IModule
    {
        string ModuleName { get; }
        Task Invoke(IArguments args);
    }
}
