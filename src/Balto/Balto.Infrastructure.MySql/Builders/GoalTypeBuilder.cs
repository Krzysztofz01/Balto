using Balto.Domain.Goals;
using Balto.Infrastructure.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Infrastructure.MySql.Builders
{
    public class GoalTypeBuilder
    {
        public GoalTypeBuilder(EntityTypeBuilder<Goal> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.OwnsOne(e => e.OwnerId);
            builder.OwnsOne(e => e.Title).Property(v => v.Value).HasMaxLength(100);
            builder.Navigation(e => e.Title).IsRequired();
            builder.OwnsOne(e => e.Description).Property(v => v.Value).HasMaxLength(300);
            builder.Navigation(e => e.Description).IsRequired();
            builder.OwnsOne(e => e.Priority);
            builder.OwnsRequiredOne(e => e.Color);
            builder.OwnsOne(e => e.StartingDate);
            builder.OwnsRequiredOne(e => e.Deadline);
            builder.OwnsOne(e => e.IsRecurring);
            builder.OwnsOne(e => e.Status);

            builder.OwnsMany(e => e.Tags, e =>
            {
                e.WithOwner().HasForeignKey("goalId");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).ValueGeneratedNever();
                e.OwnsOne(e => e.TagId);

                e.Property(e => e.DeletedAt).HasDefaultValue(null);
            });

            builder.Property(e => e.DeletedAt).HasDefaultValue(null);
        }
    }
}
