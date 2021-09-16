using Balto.Domain.Aggregates.Team;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Infrastructure.SqlServer.Builders
{
    public class TeamAggregateBuilder
    {
        public TeamAggregateBuilder(EntityTypeBuilder<Team> builder)
        {
            //Property mapping
            builder.HasKey(e => e.TeamId);
            builder.OwnsOne(e => e.Id).HasIndex(e => e.Value).IsUnique();
            builder.OwnsOne(e => e.Name);
            builder.OwnsOne(e => e.Color);

            //Delete property
            builder.Property(e => e.DeletedAt).HasDefaultValue(null);
        }
    }
}
