using System;
using System.Threading.Tasks;

namespace Balto.Domain.Identities
{
    public interface IIdentityRepository
    {
        Task Add(Identity goal);
        Task<Identity> Get(Guid id);
        Task<bool> Exists(Guid id);
    }
}
