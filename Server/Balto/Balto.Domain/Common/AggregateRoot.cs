using System.Collections.Generic;
using System.Linq;

namespace Balto.Domain.Common
{
    public abstract class AggregateRoot<TId> : AuditableEntity, IInternalEventHandler where TId : Value<TId>
    {
        private readonly List<object> _changes;

        public TId Id { get; protected set; }

        protected AggregateRoot() => _changes = new List<object>();

        protected abstract void When(object @event);
        protected abstract void EnsureValidState();

        protected void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            _changes.Add(@event);
        }

        public IEnumerable<object> GetChanges() => _changes.AsEnumerable();

        public void ClearChanges() => _changes.Clear();

        protected void ApplyToEntity(IInternalEventHandler entity, object @event) => entity?.Handle(@event);

        public void Handle(object @event) => When(@event);
    }
}
