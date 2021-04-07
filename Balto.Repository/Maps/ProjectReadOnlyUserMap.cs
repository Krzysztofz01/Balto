using Balto.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Repository.Maps
{
    public class ProjectReadOnlyUserMap
    {
        public ProjectReadOnlyUserMap(EntityTypeBuilder<ProjectReadOnlyUser> entityBuilder)
        {
            entityBuilder.HasKey(p => new { p.ProjectId, p.UserId });

            //Relations
            //Many(User) to Many(Project) as readonly using helper table

            //One(User) to Many(Project as readonly)
            entityBuilder
                .HasOne<User>(p => p.User)
                .WithMany(u => u.SharedReadOnlyProjects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            //One(Project) to Many(User as readonly)
            entityBuilder
                .HasOne<Project>(p => p.Project)
                .WithMany(p => p.ReadOnlyUsers)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
