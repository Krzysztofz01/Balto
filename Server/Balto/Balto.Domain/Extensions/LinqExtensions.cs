using System;
using System.Collections.Generic;
using System.Linq;

namespace Balto.Domain.Extensions
{
    public static class LinqExtensions
    {
        public static int MaxOrDefaultOrdinal<T>(this IEnumerable<T> source, Func<T, int> selector)
        {
            return source.Any() ? source.Max(selector) + 1 : 0;
        }
    }
}
