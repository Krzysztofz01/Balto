using Balto.Domain.Goals;
using Balto.Domain.Identities;
using Balto.Domain.Notes;
using Balto.Domain.Projects;
using Balto.Domain.Tags;
using Balto.Domain.Team;
using System;
using System.Linq;

namespace Balto.Infrastructure.Core.Abstraction
{
    public interface IDataAccess
    {
        IQueryable<Identity> Identities { get; }
        IQueryable<Project> Projects { get; }
        IQueryable<Goal> Goals { get; }
        IQueryable<Tag> Tags { get; }
        IQueryable<Note> Notes { get; }
        IQueryable<Team> Teams { get; }

        [Obsolete]
        IQueryable<Identity> IdentitiesTracked { get; }
        [Obsolete]
        IQueryable<Project> ProjectsTracked { get; }
        [Obsolete]
        IQueryable<Goal> GoalsTracked { get; }
        [Obsolete]
        IQueryable<Tag> TagsTracked { get; }
        [Obsolete]
        IQueryable<Note> NotesTracked { get; }
        [Obsolete]
        IQueryable<Team> TeamsTracked { get; }
    }
}
