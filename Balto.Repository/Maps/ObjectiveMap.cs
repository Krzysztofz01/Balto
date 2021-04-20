using Balto.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Repository.Maps
{
    public class ObjectiveMap
    {
        public ObjectiveMap(EntityTypeBuilder<Objective> entityBuilder)
        {
            //Base entity
            entityBuilder.HasKey(p => p.Id);
            entityBuilder.Property(p => p.Id).UseIdentityColumn().Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

            entityBuilder.Property(p => p.AddDate).IsRequired().HasDefaultValueSql("getdate()");
            entityBuilder.Property(p => p.EditDate).IsRequired().HasDefaultValueSql("getdate()");

            entityBuilder.Property(o => o.Name).IsRequired();
            entityBuilder.Property(o => o.Description).IsRequired().HasDefaultValue("");
            entityBuilder.Property(o => o.Finished).IsRequired().HasDefaultValue(false);
            entityBuilder.Property(o => o.StartingDate).IsRequired().HasDefaultValueSql("getdate()");
            entityBuilder.Property(o => o.EndingDate).IsRequired();

            //Relations
            //One(User) to Many(Objective)
            entityBuilder
                .HasOne<User>(o => o.User)
                .WithMany(u => u.Objectives)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
