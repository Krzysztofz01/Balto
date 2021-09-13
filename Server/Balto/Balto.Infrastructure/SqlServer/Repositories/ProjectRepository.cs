using Balto.Domain.Aggregates.Project;
using Balto.Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Balto.Infrastructure.SqlServer.Repositories
{
    public class ProjectRepository : IProjectRepository, IDisposable
    {
        private readonly BaltoDbContext _dbContext;

        public ProjectRepository(BaltoDbContext dbContext)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task Add(Project entity)
        {
            await _dbContext.Projects.AddAsync(entity);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<bool> Exists(ProjectId id)
        {
            return await _dbContext.Projects.AnyAsync(e => e.Id.Value == id.Value);
        }

        public async Task<Project> Load(ProjectId id)
        {
            return await _dbContext.Projects.SingleAsync(e => e.Id.Value == id.Value);
        }
    }
}
