using Balto.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Repository.Maps
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityBuilder)
        {
            //Base entity
            entityBuilder.HasKey(p => p.Id);
            entityBuilder.Property(p => p.AddDate).IsRequired().HasDefaultValueSql("getdate()");
            entityBuilder.Property(p => p.EditDate).IsRequired().HasDefaultValueSql("getdate()");

            entityBuilder.HasIndex(u => u.Email).IsUnique();
            entityBuilder.Property(u => u.Password).IsRequired();
            entityBuilder.Property(u => u.LastLoginDate).IsRequired().HasDefaultValueSql("getdate()");
            entityBuilder.Property(u => u.LastLoginIp).IsRequired().HasDefaultValue("");
        }
    }
}
