using Balto.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Repository.Maps
{
    class NoteReadOnlyUserMap
    {
        public NoteReadOnlyUserMap(EntityTypeBuilder<NoteReadOnlyUser> entityBuilder)
        {
            //Base entity (without single Id key)
            entityBuilder.Property(p => p.AddDate).IsRequired().HasDefaultValueSql("getdate()");
            entityBuilder.Property(p => p.EditDate).IsRequired().HasDefaultValueSql("getdate()");

            //Base entity Id property and keys related to relationship items
            entityBuilder.HasKey(n => new { n.Id, n.NoteId, n.UserId });

            //Relations
            //Many(User) to Many(Note) as readonly using helper table

            //One(User) to Many(Note as readonly)
            entityBuilder
                .HasOne<User>(n => n.User)
                .WithMany(u => u.SharedReadOnlyNotes)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            //One(Note) to Many(User as readonly)
            entityBuilder
                .HasOne<Note>(n => n.Note)
                .WithMany(n => n.ReadOnlyUsers)
                .HasForeignKey(n => n.NoteId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
