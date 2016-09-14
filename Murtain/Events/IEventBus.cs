using System;
using System.Threading.Tasks;

using Murtain.Dependency;
using Murtain.Events.Handlers;

namespace Murtain.Events
{
    /// <summary>
    /// Defines interface of the event bus.
    /// </summary>
    public interface IEventBus
    {

        /// <summary>
        /// Registers to an event.
        /// Given action is called for all event occurrences.
        /// </summary>
        /// <param name="action">Action to handle events</param>
        /// <typeparam name="TDomainEvent">Event type</typeparam>
        IDisposable Register<TDomainEvent>(Action<TDomainEvent> action) where TDomainEvent : IDomainEvent;

        /// <summary>
        /// Registers to an event. 
        /// Same (given) instance of the handler is used for all event occurrences.
        /// </summary>
        /// <typeparam name="TDomainEvent">Event type</typeparam>
        /// <param name="handler">Object to handle the event</param>
        IDisposable Register<TDomainEvent>(IEventHandler<TDomainEvent> handler) where TDomainEvent : IDomainEvent;

        /// <summary>
        /// Registers to an event.
        /// A new instance of <see cref="THandler"/> object is created for every event occurrence.
        /// </summary>
        /// <typeparam name="TDomainEvent">Event type</typeparam>
        /// <typeparam name="THandler">Type of the event handler</typeparam>
        IDisposable Register<TDomainEvent, THandler>() where TDomainEvent : IDomainEvent where THandler : IEventHandler<TDomainEvent>, new();

        /// <summary>
        /// Registers to an event.
        /// Same (given) instance of the handler is used for all event occurrences.
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <param name="handler">Object to handle the event</param>
        IDisposable Register(Type eventType, IEventHandler handler);



        /// <summary>
        /// Unregisters from an event.
        /// </summary>
        /// <typeparam name="TDomainEvent">Event type</typeparam>
        /// <param name="action"></param>
        void Unregister<TDomainEvent>(Action<TDomainEvent> action) where TDomainEvent : IDomainEvent;

        /// <summary>
        /// Unregisters from an event.
        /// </summary>
        /// <typeparam name="TDomainEvent">Event type</typeparam>
        /// <param name="handler">Handler object that is registered before</param>
        void Unregister<TDomainEvent>(IEventHandler<TDomainEvent> handler) where TDomainEvent : IDomainEvent;

        /// <summary>
        /// Unregisters from an event.
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <param name="handler">Handler object that is registered before</param>
        void Unregister(Type eventType, IEventHandler handler);

        /// <summary>
        /// Unregisters all event handlers of given event type.
        /// </summary>
        /// <typeparam name="TDomainEvent">Event type</typeparam>
        void UnregisterAll<TDomainEvent>() where TDomainEvent : IDomainEvent;

        /// <summary>
        /// Unregisters all event handlers of given event type.
        /// </summary>
        /// <param name="eventType">Event type</param>
        void UnregisterAll(Type eventType);


        /// <summary>
        /// Triggers an event.
        /// </summary>
        /// <typeparam name="TDomainEvent">Event type</typeparam>
        /// <param name="eventData">Related data for the event</param>
        void Trigger<TDomainEvent>(TDomainEvent eventData) where TDomainEvent : IDomainEvent;

        /// <summary>
        /// Triggers an event.
        /// </summary>
        /// <typeparam name="TDomainEvent">Event type</typeparam>
        /// <param name="eventSource">The object which triggers the event</param>
        /// <param name="eventData">Related data for the event</param>
        void Trigger<TDomainEvent>(object eventSource, TDomainEvent eventData) where TDomainEvent : IDomainEvent;

        /// <summary>
        /// Triggers an event.
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <param name="eventData">Related data for the event</param>
        void Trigger(Type eventType, IDomainEvent eventData);

        /// <summary>
        /// Triggers an event.
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <param name="eventSource">The object which triggers the event</param>
        /// <param name="eventData">Related data for the event</param>
        void Trigger(Type eventType, object eventSource, IDomainEvent eventData);

        /// <summary>
        /// Triggers an event asynchronously.
        /// </summary>
        /// <typeparam name="TDomainEvent">Event type</typeparam>
        /// <param name="eventData">Related data for the event</param>
        /// <returns>The task to handle async operation</returns>
        Task TriggerAsync<TDomainEvent>(TDomainEvent eventData) where TDomainEvent : IDomainEvent;

        /// <summary>
        /// Triggers an event asynchronously.
        /// </summary>
        /// <typeparam name="TDomainEvent">Event type</typeparam>
        /// <param name="eventSource">The object which triggers the event</param>
        /// <param name="eventData">Related data for the event</param>
        /// <returns>The task to handle async operation</returns>
        Task TriggerAsync<TDomainEvent>(object eventSource, TDomainEvent eventData) where TDomainEvent : IDomainEvent;

        /// <summary>
        /// Triggers an event asynchronously.
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <param name="eventData">Related data for the event</param>
        /// <returns>The task to handle async operation</returns>
        Task TriggerAsync(Type eventType, IDomainEvent eventData);

        /// <summary>
        /// Triggers an event asynchronously.
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <param name="eventSource">The object which triggers the event</param>
        /// <param name="eventData">Related data for the event</param>
        /// <returns>The task to handle async operation</returns>
        Task TriggerAsync(Type eventType, object eventSource, IDomainEvent eventData);

    }
}