using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Objective
{
    public class ObjectiveOwnerId : Value<ObjectiveOwnerId>
    {
        public Guid Value { get; internal set; }

        protected ObjectiveOwnerId() { }

        public ObjectiveOwnerId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "User id value can not be empty!");

            Value = value;
        }

        public static implicit operator Guid(ObjectiveOwnerId self) => self.Value;

        public static ObjectiveOwnerId NoUser => new ObjectiveOwnerId();

    }
}
