using Balto.Domain.Aggregates.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Infrastructure.SqlServer.Builders
{
    public class ProjectAggregateBuilder
    {
        public ProjectAggregateBuilder(EntityTypeBuilder<Project> builder)
        {
            //Property mapping
            builder.HasKey(e => e.ProjectId);
            builder.OwnsOne(e => e.Id).HasIndex(e => e.Value).IsUnique();
            builder.OwnsOne(e => e.Title);
            builder.OwnsOne(e => e.OwnerId);
            builder.OwnsOne(e => e.TicketToken).HasIndex(e => e.Value).IsUnique();

            //Relational mapping
            builder.OwnsMany(e => e.Contributors, e =>
            {
                e.WithOwner().HasForeignKey("projectId");
                e.HasKey(e => e.ProjectContributorId);
                e.OwnsOne(e => e.Id).HasIndex(e => e.Value).IsUnique();
            });

            builder.OwnsMany(e => e.Tables, e =>
            {
                e.WithOwner().HasForeignKey("projectId");
                e.HasKey(e => e.TableId);
                e.OwnsOne(e => e.Id).HasIndex(e => e.Value).IsUnique();
                e.OwnsOne(e => e.Title);
                e.OwnsOne(e => e.Color);

                e.OwnsMany(e => e.Cards, e =>
                {
                    e.WithOwner().HasForeignKey("tableId");
                    e.HasKey(e => e.CardId);
                    e.OwnsOne(e => e.Id).HasIndex(e => e.Value).IsUnique();
                    e.OwnsOne(e => e.Title);
                    e.OwnsOne(e => e.Content);
                    e.OwnsOne(e => e.Color);
                    e.OwnsOne(e => e.CreatorId);
                    e.OwnsOne(e => e.StartingDate);
                    e.OwnsOne(e => e.Deadline);
                    e.OwnsOne(e => e.Finished);
                    e.OwnsOne(e => e.Priority);

                    e.OwnsMany(e => e.Comments, e =>
                    {
                        e.WithOwner().HasForeignKey("cardId");
                        e.HasKey(e => e.CommentId);
                        e.OwnsOne(e => e.Id).HasIndex(e => e.Value).IsUnique();
                        e.OwnsOne(e => e.Content);
                        e.OwnsOne(e => e.CreatorId);
                        e.OwnsOne(e => e.CreateDate);
                    });
                });
            });

            //Delete property
            builder.Property(e => e.DeletedAt).HasDefaultValue(null);
        }
    }
}
