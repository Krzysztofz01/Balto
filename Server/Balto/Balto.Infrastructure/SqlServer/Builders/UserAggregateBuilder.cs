using Balto.Domain.Aggregates.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Infrastructure.SqlServer.Builders
{
    public class UserAggregateBuilder
    {
        public UserAggregateBuilder(EntityTypeBuilder<User> builder)
        {
            //Property mapping
            builder.HasKey(e => e.UserId);
            builder.OwnsOne(e => e.Id).HasIndex(e => e.Value).IsUnique();
            builder.OwnsOne(e => e.Name);
            builder.OwnsOne(e => e.Email).HasIndex(e => e.Value).IsUnique();
            builder.OwnsOne(e => e.Password);
            builder.OwnsOne(e => e.TeamId);
            builder.OwnsOne(e => e.Color);
            builder.OwnsOne(e => e.LastLogin);

            //Realational mapping
            builder.OwnsMany(e => e.RefreshTokens, e =>
            {
                e.WithOwner().HasForeignKey("ownerId");
                e.HasKey(e => e.RefreshTokenId);
                e.OwnsOne(e => e.Id).HasIndex(e => e.Value).IsUnique();
            });

            //Delete property
            builder.Property(e => e.DeletedAt).HasDefaultValue(null);
        }
    }
}
