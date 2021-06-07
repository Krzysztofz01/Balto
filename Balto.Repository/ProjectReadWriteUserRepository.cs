using Balto.Domain;
using Balto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public class ProjectReadWriteUserRepository : Repository<ProjectReadWriteUser>, IProjectReadWriteUserRepository
    {
        public ProjectReadWriteUserRepository(BaltoDbContext context) : base(context)
        {
        }

        public async Task AddCollaborator(long projectId, long collaboratorId)
        {
            if(!await entities.AnyAsync(p => p.ProjectId == projectId && p.UserId == collaboratorId))
            {
                entities.Add(new ProjectReadWriteUser
                {
                    ProjectId = projectId,
                    UserId = collaboratorId
                });
            }
        }

        public IEnumerable<long?> GetProjectsIds(long userId)
        {
            return entities.Where(p => p.UserId == userId).Select(p => p.ProjectId);
        }

        public IEnumerable<long?> GetUsersIds(long projectId)
        {
            return entities.Where(p => p.ProjectId == projectId).Select(p => p.UserId);
        }

        public async Task<bool> IsRelated(long projectId, long userId)
        {
            if (await entities.AnyAsync(p => p.ProjectId == projectId && p.UserId == userId)) return true;
            return false;
        }

        public async Task<bool> RemoveCollaborator(long projectId, long collaboratorId)
        {
            var relation = await entities.SingleOrDefaultAsync(r => r.ProjectId == projectId && r.UserId == collaboratorId);
            if (relation is null) return false;

            entities.Remove(relation);
            return true;
        }
    }
}
