using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Objective
{
    public class ObjectiveId : Value<ObjectiveId>
    {
        public Guid Value { get; internal set; }

        protected ObjectiveId() { }
        public ObjectiveId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "Objective id can not be empty.");

            Value = value;
        }

        public static implicit operator Guid(ObjectiveId self) => self.Value;
        public static implicit operator ObjectiveId(string value) => new ObjectiveId(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}
