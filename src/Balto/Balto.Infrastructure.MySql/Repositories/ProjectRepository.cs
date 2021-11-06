using Balto.Domain.Projects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Balto.Infrastructure.MySql.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly BaltoDbContext _context;

        public ProjectRepository(BaltoDbContext dbContext) =>
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task Add(Project project)
        {
            _ = await _context.Projects.AddAsync(project);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Projects.AnyAsync(p => p.Id == id);
        }

        public async Task<Project> Get(Guid id)
        {
            return await _context.Projects
                .Include(p => p.Contributors)
                .Include(p => p.Tables)
                .ThenInclude(p => p.Tasks)
                .SingleAsync(p => p.Id == id);
        }
    }
}
