using Balto.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Repository.Maps
{
    public class ProjectReadWriteUserMap
    {
        public ProjectReadWriteUserMap(EntityTypeBuilder<ProjectReadWriteUser> entityBuilder)
        {
            //Base entity (without single Id key)
            entityBuilder.Property(p => p.AddDate).IsRequired().HasDefaultValueSql("getdate()");
            entityBuilder.Property(p => p.EditDate).IsRequired().HasDefaultValueSql("getdate()");

            //Base entity Id property and keys related to relationship items
            entityBuilder.HasKey(p => new { p.Id, p.ProjectId, p.UserId });

            //Relations
            //Many(User) to Many(Project) as readwrite using helper table

            //One(User) to Many(Project as readwrite)
            entityBuilder
                .HasOne<User>(p => p.User)
                .WithMany(u => u.SharedReadWriteProjects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            //One(Projecy) to Many(User as readwrite)
            entityBuilder
                .HasOne<Project>(p => p.Project)
                .WithMany(p => p.ReadWriteUsers)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
