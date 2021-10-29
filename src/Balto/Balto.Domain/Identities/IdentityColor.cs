using Balto.Domain.Shared;

namespace Balto.Domain.Identities
{
    public class IdentityColor : Color
    {
        private IdentityColor() : base() { }
        private IdentityColor(string value) : base(value) { }
        private IdentityColor(System.Drawing.Color color) : base(color) { }

        public static implicit operator string(IdentityColor color) => color.Value;
        public static implicit operator System.Drawing.Color(IdentityColor color) => color.ToColor();

        public static IdentityColor Default => new IdentityColor(_defaultValue);
        public static IdentityColor FromString(string color) => new IdentityColor(color);
        public static IdentityColor FromColor(System.Drawing.Color color) => new IdentityColor(color);
    }
}
