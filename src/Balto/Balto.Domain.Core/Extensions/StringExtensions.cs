namespace Balto.Domain.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        public static bool IsLengthBetween(this string value, int min, int max)
        {
            return (value.Length > min) && (value.Length < max);
        }

        public static bool IsLengthLess(this string value, int max)
        {
            return value.Length < max;
        }

        public static bool IsLength(this string value, int expected)
        {
            return value.Length == expected;
        }
    }
}
