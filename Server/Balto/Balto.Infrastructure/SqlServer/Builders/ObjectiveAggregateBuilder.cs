using Balto.Domain.Aggregates.Objective;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Infrastructure.SqlServer.Builders
{
    public class ObjectiveAggregateBuilder
    {
        public ObjectiveAggregateBuilder(EntityTypeBuilder<Objective> builder)
        {
            //Property mapping
            builder.HasKey(e => e.ObjectiveId);
            builder.OwnsOne(e => e.Id).HasIndex(e => e.Value).IsUnique();
            builder.OwnsOne(e => e.Title);
            builder.OwnsOne(e => e.Description);
            builder.OwnsOne(e => e.Priority);
            builder.OwnsOne(e => e.Periodicity);
            builder.OwnsOne(e => e.StartingDate);
            builder.OwnsOne(e => e.EndingDate);
            builder.OwnsOne(e => e.FinishState);
            builder.OwnsOne(e => e.OwnerId);

            //Delete property
            builder.Property(e => e.DeletedAt).HasDefaultValue(null);
        }
    }
}
