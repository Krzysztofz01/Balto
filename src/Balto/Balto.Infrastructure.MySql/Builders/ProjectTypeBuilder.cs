using Balto.Domain.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Infrastructure.MySql.Builders
{
    public class ProjectTypeBuilder
    {
        public ProjectTypeBuilder(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.OwnsOne(e => e.Title).Property(v => v.Value).HasMaxLength(100);
            builder.OwnsOne(e => e.OwnerId);
            builder.OwnsOne(e => e.TicketToken).HasIndex(v => v.Value).IsUnique();

            builder.OwnsMany(e => e.Contributors, e =>
            {
                e.WithOwner().HasForeignKey("projectId");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).ValueGeneratedNever();
                e.OwnsOne(e => e.IdentityId);
                e.OwnsOne(e => e.Role);

                e.Property(e => e.DeletedAt).HasDefaultValue(null);
            });

            builder.OwnsMany(e => e.Tables, e =>
            {
                e.WithOwner().HasForeignKey("projectId");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).ValueGeneratedNever();
                e.OwnsOne(e => e.Title).Property(v => v.Value).HasMaxLength(100);
                e.OwnsOne(e => e.Color);

                e.OwnsMany(e => e.Tasks, e =>
                {
                    e.WithOwner().HasForeignKey("tableId");
                    e.HasKey(e => e.Id);
                    e.Property(e => e.Id).ValueGeneratedNever();
                    e.OwnsOne(e => e.Title).Property(v => v.Value).HasMaxLength(100);
                    e.OwnsOne(e => e.Content).Property(v => v.Value).HasMaxLength(300);
                    e.OwnsOne(e => e.Color);
                    e.OwnsOne(e => e.CreatorId);
                    e.OwnsOne(e => e.AssignedContributorId);
                    e.OwnsOne(e => e.StartingDate);
                    e.OwnsOne(e => e.Deadline);
                    e.OwnsOne(e => e.Status);
                    e.OwnsOne(e => e.Priority);
                    e.OwnsOne(e => e.OrdinalNumber);

                    e.OwnsMany(e => e.Tags, e =>
                    {
                        e.WithOwner().HasForeignKey("taskId");
                        e.HasKey(e => e.Id);
                        e.Property(e => e.Id).ValueGeneratedNever();
                        e.OwnsOne(e => e.TagId);

                        e.Property(e => e.DeletedAt).HasDefaultValue(null);
                    });

                    e.Property(e => e.DeletedAt).HasDefaultValue(null);
                });

                e.Property(e => e.DeletedAt).HasDefaultValue(null);
            });

            builder.Property(e => e.DeletedAt).HasDefaultValue(null);
        }
    }
}
