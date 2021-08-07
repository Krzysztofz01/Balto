using System.Threading.Tasks;

namespace Balto.Domain.Common
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
