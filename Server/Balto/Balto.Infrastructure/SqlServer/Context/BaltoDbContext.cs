using Balto.Domain.Aggregates.Objective;
using Balto.Domain.Aggregates.User;
using Balto.Domain.Common;
using Balto.Infrastructure.Authentication;
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
        private IRequestContext _requestContext;

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Objective> Objectives { get; set; }

        public BaltoDbContext(DbContextOptions options, IRequestContext requestContext) : base(options)
        {
            _requestContext = requestContext ??
                throw new ArgumentNullException(nameof(requestContext));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserAggregateBuilder(modelBuilder.Entity<User>());

            new ObjectiveAggregateBuilder(modelBuilder.Entity<Objective>());
            modelBuilder.Entity<Objective>()
                .HasQueryFilter(e => _requestContext.UserHasAccess(e.OwnerId));
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
