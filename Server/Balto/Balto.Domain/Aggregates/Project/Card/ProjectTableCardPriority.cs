using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCardPriority : Value<ProjectTableCardPriority>
    {
        public CardPriorityType Value { get; private set; }

        protected ProjectTableCardPriority() { }
        protected ProjectTableCardPriority(CardPriorityType value) => Value = value;

        public static implicit operator int(ProjectTableCardPriority priority) => (int)priority.Value;
        public static implicit operator string(ProjectTableCardPriority priority) => Enum.GetName(typeof(CardPriorityType), priority.Value);
        public static implicit operator CardPriorityType(ProjectTableCardPriority priority) => priority.Value;

        public static ProjectTableCardPriority Set(CardPriorityType priority) => new ProjectTableCardPriority(priority);
        public static ProjectTableCardPriority Default => new ProjectTableCardPriority(CardPriorityType.Default);
    }

    public enum CardPriorityType
    {
        Default = 10,
        Important = 20,
        Crucial = 30
    }
}
