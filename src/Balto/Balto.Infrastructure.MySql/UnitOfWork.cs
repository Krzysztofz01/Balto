using Balto.Domain.Goals;
using Balto.Domain.Identities;
using Balto.Domain.Notes;
using Balto.Domain.Projects;
using Balto.Domain.Tags;
using Balto.Domain.Teams;
using Balto.Infrastructure.Core.Abstraction;
using Balto.Infrastructure.MySql.Repositories;
using System;
using System.Threading.Tasks;

namespace Balto.Infrastructure.MySql
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaltoDbContext _context;

        private IIdentityRepository _identityRepository;
        private IGoalRepository _goalRepository;
        private IProjectRepository _projectRepository;
        private ITagRepository _tagRepository;
        private INoteRepository _noteRepository;
        private ITeamRepository _teamRepository;

        public UnitOfWork(BaltoDbContext dbContext) =>
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public IIdentityRepository IdentityRepository
        {
            get
            {
                if (_identityRepository is null) _identityRepository = new IdentityRepository(_context);

                return _identityRepository;
            }
        }

        public IGoalRepository GoalRepository
        {
            get
            {
                if (_goalRepository is null) _goalRepository = new GoalRepository(_context);

                return _goalRepository;
            }
        }

        public IProjectRepository ProjectRepository
        {
            get
            {
                if (_projectRepository is null) _projectRepository = new ProjectRepository(_context);

                return _projectRepository;
            }
        }

        public ITagRepository TagRepository
        {
            get
            {
                if (_tagRepository is null) _tagRepository = new TagRepository(_context);

                return _tagRepository;
            }
        }

        public INoteRepository NoteRepository
        {
            get
            {
                if (_noteRepository is null) _noteRepository = new NoteRepository(_context);

                return _noteRepository;
            }
        }

        public ITeamRepository TeamRepository
        {
            get
            {
                if (_teamRepository is null) _teamRepository = new TeamRepository(_context);

                return _teamRepository;
            }
        }

        public async Task Commit()
        {
            _ = await _context.SaveChangesAsync();
        }
    }
}
