using Balto.Domain.Aggregates.Objective;
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

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Objective> Objectives { get; set; }

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
            //Users
            new UserAggregateBuilder(modelBuilder.Entity<User>());
            modelBuilder.Entity<User>()
                .HasQueryFilter(e => e.DeletedAt == null);

            //Objectives
            new ObjectiveAggregateBuilder(modelBuilder.Entity<Objective>());
            modelBuilder.Entity<Objective>()
                .HasQueryFilter(e => e.OwnerId.Value == _requestAuthorizationHandler.GetUserGuid() && e.DeletedAt == null);
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
