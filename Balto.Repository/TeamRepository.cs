using Balto.Domain;
using Balto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(BaltoDbContext context) : base(context)
        {
        }

        public IEnumerable<Team> AllTeams()
        {
            return entities.Include(t => t.Users);
        }

        public async Task<Team> GetUsersTeam(long userId)
        {
            return await entities
                .Include(t => t.Users)
                .SingleOrDefaultAsync(t => t.Users.Any(u => u.Id == userId));
        }

        public async Task<Team> SingleTeam(long teamId)
        {
            return await entities
                .Include(t => t.Users)
                .SingleOrDefaultAsync(t => t.Id == teamId);
        }
    }
}
