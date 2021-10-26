using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;

namespace Balto.Domain.Shared
{
    public abstract class Color : ValueObject<Color>
    {
        private const int _hexColorStringLength = 7;
        protected const string _defaultValue = "#FFFFFF";

        public string Value { get; protected set; }

        protected Color() { }
        
        protected Color(string value)
        {
            if (!value.IsLength(_hexColorStringLength) || value.IsEmpty())
                throw new ValueObjectValidationException("Invalid hexadecimal color string length.");

            Value = value;
        }

        protected Color(System.Drawing.Color value)
        {
            string hexValue = value.ToHexString();

            if (!hexValue.IsLength(_hexColorStringLength) || hexValue.IsEmpty())
                throw new ValueObjectValidationException("Invalid hexadecimal color string length.");

            Value = hexValue;
        }

        public static implicit operator string(Color color) => color.Value;
        public static implicit operator System.Drawing.Color(Color color) => System.Drawing.ColorTranslator.FromHtml(color.Value);
    }
}
