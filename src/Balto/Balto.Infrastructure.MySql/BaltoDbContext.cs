using Balto.Domain.Core.Model;
using Balto.Domain.Goals;
using Balto.Domain.Identities;
using Balto.Domain.Projects;
using Balto.Infrastructure.Core.Abstraction;
using Balto.Infrastructure.MySql.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Balto.Infrastructure.MySql
{
    public class BaltoDbContext : DbContext
    {
        private const string _schemaName = "balto";

        private readonly IScopeWrapperService _scope;

        public virtual DbSet<Identity> Identities { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        public BaltoDbContext(DbContextOptions<BaltoDbContext> options) : base(options) { }
        public BaltoDbContext(DbContextOptions<BaltoDbContext> options, IScopeWrapperService scopeWrapperService) : base(options) =>
            _scope = scopeWrapperService ?? throw new ArgumentNullException(nameof(scopeWrapperService));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_schemaName);

            //Identity aggregate
            new IdentityTypeBuilder(modelBuilder.Entity<Identity>());
            modelBuilder.Entity<Identity>().HasQueryFilter(i =>
                i.DeletedAt == null);

            //Goal aggregate
            new GoalTypeBuilder(modelBuilder.Entity<Goal>());
            modelBuilder.Entity<Goal>().HasQueryFilter(g =>
                g.DeletedAt == null &&
                g.OwnerId == _scope.GetUserId());

            //Project aggregate
            new ProjectTypeBuilder(modelBuilder.Entity<Project>());
            modelBuilder.Entity<Project>().HasQueryFilter(p =>
                p.DeletedAt == null && (
                    p.OwnerId == _scope.GetUserId() ||
                    p.Contributors.Any(c => c.IdentityId.Value == _scope.GetUserId())));
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entites = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditableSubject &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach(var entity in entites)
            {
                ((AuditableSubject)entity.Entity).UpdatedAt = DateTime.Now;

                if (entity.State == EntityState.Added)
                {
                    ((AuditableSubject)entity.Entity).UpdatedAt = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
