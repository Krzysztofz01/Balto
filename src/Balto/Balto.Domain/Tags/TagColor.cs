using Balto.Domain.Shared;

namespace Balto.Domain.Tags
{
    public class TagColor : Color
    {
        private TagColor() : base() { }
        private TagColor(string value) : base(value) { }
        private TagColor(System.Drawing.Color color) : base(color) { }

        public static implicit operator string(TagColor color) => color.Value;
        public static implicit operator System.Drawing.Color(TagColor color) => color.ToColor();

        public static TagColor Default => new TagColor(_defaultValue);
        public static TagColor FromString(string color) => new TagColor(color);
        public static TagColor FromColor(System.Drawing.Color color) => new TagColor(color);
    }
}
