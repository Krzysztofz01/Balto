using System;

namespace Balto.Domain.Core.Model
{
    public abstract class Entity : AuditableObject
    {
        public Guid Id { get; protected set; }
        public DateTime? DeletedAt { get; set; }

        protected abstract void Validate();

        public override bool Equals(object obj)
        {
            var compare = obj as Entity;

            if (ReferenceEquals(this, compare)) return true;
            if (compare is null) return false;

            return Id.Equals(compare.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public static explicit operator Guid(Entity entity) => entity.Id;

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Entity Id: {Id}";
        }
    }
}
