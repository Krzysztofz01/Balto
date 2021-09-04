using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Objective
{
    public class ObjectivePeriodicity : Value<ObjectivePeriodicity>
    {
        public ObjectivePeriodicityType Value { get; private set; }

        protected ObjectivePeriodicity() { }
        protected ObjectivePeriodicity(ObjectivePeriodicityType type)
        {
            Value = type;
        }

        public static implicit operator int(ObjectivePeriodicity periodicity) => (int)periodicity.Value;
        public static implicit operator string(ObjectivePeriodicity periodicity) => Enum.GetName(typeof(ObjectivePeriodicityType), periodicity.Value);
        public static implicit operator ObjectivePeriodicityType(ObjectivePeriodicity periodicity) => periodicity.Value;

        public static ObjectivePeriodicity Set(ObjectivePeriodicityType type) => new ObjectivePeriodicity(type);
        public static ObjectivePeriodicity Default => new ObjectivePeriodicity(ObjectivePeriodicityType.Default);
    }

    public enum ObjectivePeriodicityType
    {
        Default = 0,
        Daily = 1,
        Weekly = 2
    }
}
