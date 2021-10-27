using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Shared
{
    public abstract class Priority : ValueObject<Priority>
    {
        public PriorityTypes Value { get; protected set; }

        protected Priority() { }
        protected Priority(PriorityTypes priorityTypes)
        {
            Value = priorityTypes;
        }

        protected string GetName() => Enum.GetName(typeof(PriorityTypes), Value);
    }

    public enum PriorityTypes
    {
        Smallest,
        Low,
        Medium,
        High,
        Crucial
    }
}
