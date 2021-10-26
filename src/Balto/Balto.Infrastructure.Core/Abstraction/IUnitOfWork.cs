using System.Threading.Tasks;

namespace Balto.Infrastructure.Core.Abstraction
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
