using Balto.Domain;
using Balto.Repository.Context;

namespace Balto.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BaltoDbContext context) : base(context)
        {
        }
    }
}
