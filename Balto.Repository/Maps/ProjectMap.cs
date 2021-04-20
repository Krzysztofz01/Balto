using Balto.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Repository.Maps
{
    public class ProjectMap
    {
        public ProjectMap(EntityTypeBuilder<Project> entityBuilder)
        {
            //Base entity
            entityBuilder.HasKey(p => p.Id);
            entityBuilder.Property(p => p.Id).UseIdentityColumn().Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

            entityBuilder.Property(p => p.AddDate).IsRequired().HasDefaultValueSql("getdate()");
            entityBuilder.Property(p => p.EditDate).IsRequired().HasDefaultValueSql("getdate()");

            entityBuilder.Property(p => p.Name).IsRequired();

            //Relations
            //One(User) to Many(Project) as owner
            entityBuilder
                .HasOne<User>(p => p.Owner)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
