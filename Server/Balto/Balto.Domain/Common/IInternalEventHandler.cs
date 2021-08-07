namespace Balto.Domain.Common
{
    public interface IInternalEventHandler
    {
        void Handle(object @event);
    }
}
