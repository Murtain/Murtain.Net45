using System;
using Murtain.Dependency;

namespace Murtain.Events.Handlers
{
    /// <summary>
    /// This event handler is an adapter to be able to use an action as <see cref="IEventHandler{TEventData}"/> implementation.
    /// </summary>
    /// <typeparam name="TDomainEvent">Event type</typeparam>
    internal class ActionEventHandler<TDomainEvent> :
        IEventHandler<TDomainEvent>,
        IDependency
    {
        /// <summary>
        /// Action to handle the event.
        /// </summary>
        public Action<TDomainEvent> Action { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="ActionEventHandler{TEventData}"/>.
        /// </summary>
        /// <param name="handler">Action to handle the event</param>
        public ActionEventHandler(Action<TDomainEvent> handler)
        {
            Action = handler;
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(TDomainEvent eventData)
        {
            Action(eventData);
        }
    }
}