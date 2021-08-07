using Balto.Domain.Common;
using Balto.Domain.Exceptions;

namespace Balto.Domain.Extensions
{
    public static class ValueExtensions
    {
        public static void MustNotBeNull<T>(this Value<T> value) where T : Value<T>
        {
            if (value is null) throw new InvalidValueObjectException(typeof(T), "can not be null.");
        }

        public static void MustBe<T>(this Value<T> value) where T : Value<T>
        {
            if (value is null) throw new InvalidValueObjectException(typeof(T), "can not be null.");
        }

    }
}
