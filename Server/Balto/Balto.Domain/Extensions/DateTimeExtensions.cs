using System;

namespace Balto.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime DayEndToday(this DateTime dateTime)
        {
            return dateTime.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        public static DateTime DayEndWeek(this DateTime dateTime)
        {
            return dateTime.DayEndToday().AddDays(7);
        }
    }
}
