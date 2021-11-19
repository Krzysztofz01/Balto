using Balto.Domain.Goals;
using Balto.Domain.Identities;
using Balto.Domain.Projects;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Balto.Infrastructure.MySql
{
    public class DataAccess : IDataAccess
    {
        private readonly BaltoDbContext _context;

        public DataAccess(BaltoDbContext dbContext) =>
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public IQueryable<Identity> Identities => _context.Identities.AsNoTracking();

        public IQueryable<Project> Projects => _context.Projects.AsNoTracking();

        public IQueryable<Goal> Goals => _context.Goals.AsNoTracking();

        public IQueryable<Identity> IdentitiesTracked => _context.Identities;

        public IQueryable<Project> ProjectsTracked => _context.Projects;

        public IQueryable<Goal> GoalsTracked => _context.Goals;
    }
}
