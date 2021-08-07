using Balto.Domain.Common;
using Balto.Domain.Extensions;

namespace Balto.Domain.Shared
{
    public class Color : Value<Color>
    {
        protected const string _defaultValue = "#FFF";

        public string Value { get; internal set; }

        protected Color() { }

        protected Color(string value)
        {
            Value = _defaultValue;

            if (!value.IsEmpty()) Value = value;
        }

        public static implicit operator string(Color color) => color.Value;

        public static Color Default => new Color(_defaultValue);
        public static Color Set(string value) => new Color(value);
    }
}
