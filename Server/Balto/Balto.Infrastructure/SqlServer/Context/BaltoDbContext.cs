using Balto.Domain.Aggregates.Note;
using Balto.Domain.Aggregates.Objective;
using Balto.Domain.Aggregates.Project;
using Balto.Domain.Aggregates.User;
using Balto.Domain.Common;
using Balto.Infrastructure.Abstraction;
using Balto.Infrastructure.SqlServer.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Balto.Infrastructure.SqlServer.Context
{
    public class BaltoDbContext : DbContext
    {
        private readonly IRequestAuthorizationHandler _requestAuthorizationHandler;

        private const string _schemaName = "balto";

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Objective> Objectives { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        public BaltoDbContext(DbContextOptions<BaltoDbContext> options) : base(options)
        {
        }

        public BaltoDbContext(DbContextOptions<BaltoDbContext> options, IRequestAuthorizationHandler requestAuthorizationHandler) : base(options)
        {
            _requestAuthorizationHandler = requestAuthorizationHandler ??
                throw new ArgumentNullException(nameof(requestAuthorizationHandler));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Global
            modelBuilder.HasDefaultSchema(_schemaName);

            //Users
            new UserAggregateBuilder(modelBuilder.Entity<User>());
            modelBuilder.Entity<User>().HasQueryFilter(e => 
                e.DeletedAt == null);

            //Objectives
            new ObjectiveAggregateBuilder(modelBuilder.Entity<Objective>());
            modelBuilder.Entity<Objective>().HasQueryFilter(e =>
                e.OwnerId.Value == _requestAuthorizationHandler.GetUserGuid() &&
                e.DeletedAt == null);

            //Notes
            new NoteAggregateBuilder(modelBuilder.Entity<Note>());
            modelBuilder.Entity<Note>().HasQueryFilter(e =>
                (e.OwnerId.Value == _requestAuthorizationHandler.GetUserGuid() ||
                    e.Contributors.Any(c => c.Id.Value == _requestAuthorizationHandler.GetUserGuid())) &&
                e.DeletedAt == null);

            //Projects
            new ProjectAggregateBuilder(modelBuilder.Entity<Project>());
            modelBuilder.Entity<Project>().HasQueryFilter(e =>
                (e.OwnerId.Value == _requestAuthorizationHandler.GetUserGuid() ||
                    e.Contributors.Any(c => c.Id.Value == _requestAuthorizationHandler.GetUserGuid())) &&
                e.DeletedAt == null);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entires = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditableEntity &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach(var entity in entires)
            {
                ((AuditableEntity)entity.Entity).SetModifiedDate();

                if (entity.State == EntityState.Added)
                {
                    ((AuditableEntity)entity.Entity).SetCreateDate();
                }
            }
            
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
