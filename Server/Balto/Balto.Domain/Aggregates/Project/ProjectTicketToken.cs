﻿using Balto.Domain.Common;
using Balto.Domain.Exceptions;
using Balto.Domain.Extensions;
using System;
using System.Security.Cryptography;

namespace Balto.Domain.Aggregates.Project
{
    public class ProjectTicketToken : Value<ProjectTicketToken>
    {
        public string Value { get; private set; }

        protected ProjectTicketToken() 
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();

            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            Value = Convert.ToBase64String(randomBytes);

            if (Value.IsEmpty())
                throw new InvalidValueObjectException(GetType(), "Invalid token generated by the crypto provider.");
        }
        protected ProjectTicketToken(string value) => Value = value;

        public static implicit operator string(ProjectTicketToken token) => token.Value;

        public static ProjectTicketToken Generate() => new ProjectTicketToken();
        public static ProjectTicketToken Empty => new ProjectTicketToken(string.Empty);
    }
}
