using System.Drawing;

namespace Balto.Domain.Core.Extensions
{
    public static class ColorExtensions
    {
        public static string ToHexString(this Color value)
        {
            return $"#{value.R:X2}{value.G:X2}{value.B:X2}";
        }
    }
}
