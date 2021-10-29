using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Shared
{
    public class Identifier : ValueObject<Identifier>
    {
        public Guid Value { get; protected set; }

        protected Identifier() { }
        protected Identifier(Guid value)
        {
            if (value == default)
                throw new ValueObjectValidationException("The identifier value can not be default.");

            Value = value;
        }
    }
}
