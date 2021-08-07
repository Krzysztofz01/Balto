using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Objective
{
    public class ObjectivePriority : Value<ObjectivePriority>
    {
        public ObjectivePriorityType Value { get; internal set; }

        protected ObjectivePriority() { }
        internal ObjectivePriority(ObjectivePriorityType value) => Value = value;

        public static ObjectivePriority Set(ObjectivePriorityType priority)
        {
            return new ObjectivePriority(priority);
        }
        
        public static implicit operator int(ObjectivePriority priority) => (int)priority.Value;
        public static implicit operator string(ObjectivePriority priority) => Enum.GetName(typeof(ObjectivePriorityType), priority.Value);
        public static implicit operator ObjectivePriorityType(ObjectivePriority priority) => priority.Value;

        public static ObjectivePriority Default => new ObjectivePriority(ObjectivePriorityType.Default);
    }

    public enum ObjectivePriorityType
    {
        Default = 10,
        Important = 20,
        Crutial = 30
    }
}
