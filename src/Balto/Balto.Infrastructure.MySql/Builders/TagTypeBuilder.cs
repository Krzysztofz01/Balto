using Balto.Domain.Tags;
using Balto.Infrastructure.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balto.Infrastructure.MySql.Builders
{
    internal class TagTypeBuilder
    {
        public TagTypeBuilder(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.OwnsOne(e => e.Title).Property(v => v.Value).HasMaxLength(100);
            builder.Navigation(e => e.Title).IsRequired();
            builder.OwnsRequiredOne(e => e.Color);

            builder.Property(e => e.DeletedAt).HasDefaultValue(null);
        }
    }
}
