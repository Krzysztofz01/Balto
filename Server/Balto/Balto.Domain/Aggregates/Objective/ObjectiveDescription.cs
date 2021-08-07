using Balto.Domain.Common;

namespace Balto.Domain.Aggregates.Objective
{
    public class ObjectiveDescription : Value<ObjectiveDescription>
    {
        public string Value { get; internal set; }

        protected ObjectiveDescription() { }
        internal ObjectiveDescription(string value) => Value = value;

        public static ObjectiveDescription FromString(string text)
        {
            return new ObjectiveDescription(text);
        }

        public static implicit operator string(ObjectiveDescription description) => description.Value;

        public static ObjectiveDescription NoDescription => new ObjectiveDescription();
    }
}
