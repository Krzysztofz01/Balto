using Balto.Domain.Aggregates.Note;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Infrastructure.SqlServer.Builders
{
    public class NoteAggregateBuilder
    {
        public NoteAggregateBuilder(EntityTypeBuilder<Note> builder)
        {
            //Property mapping
            builder.HasKey(e => e.NoteId);
            builder.OwnsOne(e => e.Id).HasIndex(e => e.Value).IsUnique();
            builder.OwnsOne(e => e.Title);
            builder.OwnsOne(e => e.Content);
            builder.OwnsOne(e => e.OwnerId);

            //Realational mapping
            builder.OwnsMany(e => e.Contributors, e =>
            {
                e.WithOwner().HasForeignKey("noteId");
                e.HasKey(e => e.NoteContributorId);
                e.OwnsOne(e => e.Id).HasIndex(e => e.Value).IsUnique();
            });

            //Delete property
            builder.Property(e => e.DeletedAt).HasDefaultValue(null);
        }
    }
}
