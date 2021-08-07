using System;

namespace Balto.Domain.Exceptions
{
    public class InvalidEntityStateException : Exception
    {
        public InvalidEntityStateException(object entity, string message)
            : base($"Entity changes { entity.GetType().Name } abandoned, { message }") 
        { 
        }
    }
}
