using Balto.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Repository.Maps
{
    public class RefreshTokenMap
    {
        public RefreshTokenMap(EntityTypeBuilder<RefreshToken> entityBuilder)
        {
            //Base entity
            entityBuilder.HasKey(p => p.Id);
            entityBuilder.Property(p => p.Id).UseIdentityColumn().Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

            entityBuilder.Property(p => p.AddDate).IsRequired().HasDefaultValueSql("getdate()");
            entityBuilder.Property(p => p.EditDate).IsRequired().HasDefaultValueSql("getdate()");

            entityBuilder.Property(p => p.Token).IsRequired();
            entityBuilder.Property(p => p.Expires).IsRequired();
            entityBuilder.Property(p => p.Created).IsRequired().HasDefaultValueSql("getdate()");
            entityBuilder.Property(p => p.CreatedByIp).IsRequired().HasDefaultValue("");
            entityBuilder.Property(p => p.RevokedByIp).IsRequired().HasDefaultValue("");
            entityBuilder.Property(p => p.IsRevoked).IsRequired().HasDefaultValue(false);

            //Relations

            entityBuilder
                .HasOne<User>(r => r.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
