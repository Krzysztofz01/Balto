using System;

namespace Balto.Domain.Common
{
    public abstract class Entity<TId> : AuditableEntity, IInternalEventHandler where TId : Value<TId>
    {
        private readonly Action<object> _applier;
        public TId Id { get; protected set; }

        protected Entity() { }
        protected Entity(Action<object> applier) => _applier = applier;

        protected abstract void When(object @event);

        protected void Apply(object @event)
        {
            When(@event);
            _applier(@event);
        }

        public void Handle(object @event) => When(@event);
    }
}
