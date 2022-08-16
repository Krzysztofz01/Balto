using Balto.Domain.Notes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Infrastructure.PostgreSQL.Builders
{
    internal class NoteTypeBuilder
    {
        private const string _textColumnTypeName = "TEXT";

        public NoteTypeBuilder(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.OwnsOne(e => e.Title).Property(v => v.Value).HasMaxLength(100);
            builder.Navigation(e => e.Title).IsRequired();
            builder.OwnsOne(e => e.Content).Property(v => v.Value).HasColumnType(_textColumnTypeName);
            builder.Navigation(e => e.Content).IsRequired();
            builder.OwnsOne(e => e.OwnerId);

            builder.OwnsMany(e => e.Contributors, e =>
            {
                e.WithOwner().HasForeignKey("noteId");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).ValueGeneratedNever();
                e.OwnsOne(e => e.IdentityId);
                e.OwnsOne(e => e.AccessRole);

                e.Property(e => e.DeletedAt).HasDefaultValue(null);
            });

            builder.OwnsMany(e => e.Snapshots, e =>
            {
                e.WithOwner().HasForeignKey("noteId");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).ValueGeneratedNever();
                e.OwnsOne(e => e.Content).Property(v => v.Value).HasColumnType(_textColumnTypeName);
                e.Navigation(e => e.Content).IsRequired();
                e.OwnsOne(e => e.CreationDate);

                e.Property(e => e.DeletedAt).HasDefaultValue(null);
            });

            builder.OwnsMany(e => e.Tags, e =>
            {
                e.WithOwner().HasForeignKey("noteId");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).ValueGeneratedNever();
                e.OwnsOne(e => e.TagId);

                e.Property(e => e.DeletedAt).HasDefaultValue(null);
            });

            builder.Property(e => e.DeletedAt).HasDefaultValue(null);
        }
    }
}
