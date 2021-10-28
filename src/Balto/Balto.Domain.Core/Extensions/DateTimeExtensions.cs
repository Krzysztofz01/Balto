using System;

namespace Balto.Domain.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Start(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, 0);
        }

        public static DateTime End(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, 999);
        }
    }
}
