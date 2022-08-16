using Balto.Domain.Teams;
using Balto.Infrastructure.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Infrastructure.PostgreSQL.Builders
{
    public class TeamTypeBuilder
    {
        public TeamTypeBuilder(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.OwnsRequiredOne(e => e.Name);
            builder.OwnsRequiredOne(e => e.Color);

            builder.OwnsMany(e => e.Members, e =>
            {
                e.WithOwner().HasForeignKey("teamId");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).ValueGeneratedNever();
                e.OwnsOne(e => e.IdentityId);

                e.Property(e => e.DeletedAt).HasDefaultValue(null);
            });

            builder.Property(e => e.DeletedAt).HasDefaultValue(null);
        }
    }
}
