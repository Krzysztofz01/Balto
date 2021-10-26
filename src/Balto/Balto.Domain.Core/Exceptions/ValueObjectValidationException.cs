﻿using System;
using System.Runtime.Serialization;

namespace Balto.Domain.Core.Exceptions
{
    public class ValueObjectValidationException : Exception
    {
        public ValueObjectValidationException()
        {
        }

        public ValueObjectValidationException(string message) : base(message)
        {
        }

        public ValueObjectValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValueObjectValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
