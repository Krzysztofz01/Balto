using Balto.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Repository.Maps
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityBuilder)
        {
            //Base entity
            entityBuilder.HasKey(p => p.Id);
            entityBuilder.Property(p => p.Id).UseIdentityColumn().Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

            entityBuilder.Property(p => p.AddDate).IsRequired().HasDefaultValueSql("getdate()");
            entityBuilder.Property(p => p.EditDate).IsRequired().HasDefaultValueSql("getdate()");

            entityBuilder.Property(u => u.Name).IsRequired();
            entityBuilder.HasIndex(u => u.Email).IsUnique();
            entityBuilder.Property(u => u.Password).IsRequired();
            entityBuilder.Property(u => u.IsLeader).HasDefaultValue(false);
            entityBuilder.Property(u => u.LastLoginDate).IsRequired().HasDefaultValueSql("getdate()");
            entityBuilder.Property(u => u.LastLoginIp).IsRequired().HasDefaultValue("");

            //Relations

            entityBuilder
                .HasOne<Team>(u => u.Team)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TeamId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        }
    }
}
