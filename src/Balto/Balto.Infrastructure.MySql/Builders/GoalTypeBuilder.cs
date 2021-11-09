using Balto.Domain.Goals;
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
            builder.OwnsOne(e => e.Description).Property(v => v.Value).HasMaxLength(300);
            builder.OwnsOne(e => e.Priority);
            builder.OwnsOne(e => e.Color);
            builder.OwnsOne(e => e.StartingDate);
            builder.OwnsOne(e => e.Deadline);
            builder.OwnsOne(e => e.IsRecurring);
            builder.OwnsOne(e => e.Status);

            builder.Property(e => e.DeletedAt).HasDefaultValue(null);
        }
    }
}
