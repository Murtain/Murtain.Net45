namespace Murtain.Events.Handlers
{
    /// <summary>
    /// Undirect base interface for all event handlers.
    /// Implement <see cref="IEventHandler{TDomainEvent}"/> instead of this one.
    /// </summary>
    public interface IEventHandler
    {
        
    }
    /// <summary>
    /// Defines an interface of a class that handles events of type <see cref="TDomainEvent"/>.
    /// </summary>
    /// <typeparam name="TDomainEvent">Event type to handle</typeparam>
    public interface IEventHandler<in TDomainEvent> : IEventHandler
    {
        /// <summary>
        /// Handler handles the event by implementing this method.
        /// </summary>
        /// <param name="domainEvent">Event data</param>
        void HandleEvent(TDomainEvent domainEvent);
    }
}