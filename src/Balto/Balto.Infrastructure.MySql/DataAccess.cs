using Balto.Domain.Goals;
using Balto.Domain.Identities;
using Balto.Domain.Notes;
using Balto.Domain.Projects;
using Balto.Domain.Tags;
using Balto.Domain.Team;
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
        
        public IQueryable<Tag> Tags => _context.Tags.AsNoTracking();

        public IQueryable<Note> Notes => _context.Notes.AsNoTracking();

        public IQueryable<Team> Teams => _context.Teams.AsNoTracking();

        public IQueryable<Identity> IdentitiesTracked => _context.Identities;

        public IQueryable<Project> ProjectsTracked => _context.Projects;

        public IQueryable<Goal> GoalsTracked => _context.Goals;

        public IQueryable<Tag> TagsTracked => _context.Tags;

        public IQueryable<Note> NotesTracked => _context.Notes;

        public IQueryable<Team> TeamsTracked => _context.Teams;
    }
}
