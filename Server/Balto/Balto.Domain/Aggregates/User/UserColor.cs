using Balto.Domain.Extensions;
using Balto.Domain.Shared;

namespace Balto.Domain.Aggregates.User
{
    public class UserColor : Color
    {
        protected UserColor() { }

        protected UserColor(string value)
        {
            Value = _defaultValue;

            if (!value.IsEmpty()) Value = value;
        }

        public static implicit operator string(UserColor color) => color.Value;

        public static new UserColor Default => new UserColor(_defaultValue);
        public static new UserColor Set(string value) => new UserColor(value);
    }
}
