using Balto.Domain.Core.Events;
using Balto.Domain.Core.Model;
using System;
using static Balto.Domain.Identities.Events;

namespace Balto.Domain.Identities.Tokens
{
    public class Token : Entity
    {
        protected override void Handle(IEventBase @event)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        private Token() { }

        public static class Factory
        {
            public static Token Create(V1.TokenCreated @event)
            {
                throw new NotImplementedException();
            }
        }

    }
}
