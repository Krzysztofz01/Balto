using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Shared
{
    public class UnrestrictedIdentifier : ValueObject<UnrestrictedIdentifier>
    {
        public Guid? Value { get; protected set; }

        protected UnrestrictedIdentifier() { }
        protected UnrestrictedIdentifier(Guid? value)
        {
            Value = value;    
        }
    }
}
