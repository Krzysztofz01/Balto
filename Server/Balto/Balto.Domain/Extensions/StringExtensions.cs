namespace Balto.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool LengthBetween(this string value, int min, int max)
        {
            return (value.Length > min) && (value.Length < max);
        }
    }
}
