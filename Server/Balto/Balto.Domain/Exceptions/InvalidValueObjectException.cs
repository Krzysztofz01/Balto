using System;

namespace Balto.Domain.Exceptions
{
    public class InvalidValueObjectException : Exception
    {
        public InvalidValueObjectException(Type type, string message)
            : base($"Value object { type.Name } { message }")
        {
        }
    }
}
