using Balto.Domain.Core.Exceptions;
using Balto.Domain.Tags;
using Xunit;

namespace Balto.Domain.Test
{
    public class TagTests
    {
        [Fact]
        public void TagShouldCreate()
        {
            Tag.Factory.Create(new Events.V1.TagCreated
            {
                Color = "#123ABC",
                Title = "Tag title"
            });
        }

        [Fact]
        public void TagShouldThrowForInvalidColor()
        {
            Assert.Throws<ValueObjectValidationException>(() =>
            {
                Tag.Factory.Create(new Events.V1.TagCreated
                {
                    Color = "@QQQZZZ",
                    Title = "Tag title"
                });
            });
        }

        [Fact]
        public void TagShouldDelete()
        {
            var tag = Tag.Factory.Create(new Events.V1.TagCreated
            {
                Color = "#123ABC",
                Title = "Tag title"
            });

            tag.Apply(new Events.V1.TagDeleted
            {
                Id = tag.Id
            });

            Assert.NotNull(tag.DeletedAt);
        }
    }
}
