using Balto.Domain.Projects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Balto.Infrastructure.PostgreSQL.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly BaltoDbContext _context;

        public ProjectRepository(BaltoDbContext dbContext) =>
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public Task Add(Project project)
        {
            _ = _context.Projects.Add(project);
            return Task.CompletedTask;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Projects
                .AsNoTracking()
                .AnyAsync(p => p.Id == id);
        }

        public async Task<Project> Get(Guid id)
        {
            return await _context.Projects
                .Include(p => p.Contributors)
                .Include(p => p.Tables)
                .ThenInclude(p => p.Tasks)
                .ThenInclude(p => p.Tags)
                .FirstAsync(p => p.Id == id);
        }

        public async Task<Project> Get(string ticketToken)
        {
            return await _context.Projects
                .Include(p => p.Contributors)
                .Include(p => p.Tables)
                .ThenInclude(p => p.Tasks)
                .ThenInclude(p => p.Tags)
                .FirstAsync(p => p.TicketToken.Value == ticketToken);
        }
    }
}
