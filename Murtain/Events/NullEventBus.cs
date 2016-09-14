using System;
using System.Threading.Tasks;

using Murtain.Events.Handlers;

namespace Murtain.Events
{
    /// <summary>
    /// An event bus that implements Null object pattern.
    /// </summary>
    public sealed class NullEventBus : IEventBus
    {
        public static IEventBus Instance { get { return _eventBus; } }
        private static readonly NullEventBus _eventBus = new NullEventBus();
        private NullEventBus()
        {
        }
        /// <inheritdoc/>
        public IDisposable Register<TEventData>(Action<TEventData> action) where TEventData : IDomainEvent
        {
            return NullDisposable.Instance;
        }

        /// <inheritdoc/>
        public IDisposable Register<TEventData>(IEventHandler<TEventData> handler) where TEventData : IDomainEvent
        {
            return NullDisposable.Instance;
        }

        /// <inheritdoc/>
        public IDisposable Register<TDomainEvent, THandler>()
            where TDomainEvent : IDomainEvent
            where THandler : IEventHandler<TDomainEvent>, new()
        {
            return NullDisposable.Instance;
        }

        /// <inheritdoc/>
        public IDisposable Register(Type eventType, IEventHandler handler)
        {
            return NullDisposable.Instance;
        }

        /// <inheritdoc/>
        public void Unregister<TEventData>(Action<TEventData> action) where TEventData : IDomainEvent
        {
        }

        /// <inheritdoc/>
        public void Unregister<TEventData>(IEventHandler<TEventData> handler) where TEventData : IDomainEvent
        {
        }

        /// <inheritdoc/>
        public void Unregister(Type eventType, IEventHandler handler)
        {
        }

        /// <inheritdoc/>
        public void UnregisterAll<TEventData>() where TEventData : IDomainEvent
        {
        }

        /// <inheritdoc/>
        public void UnregisterAll(Type eventType)
        {
        }

        /// <inheritdoc/>
        public void Trigger<TEventData>(TEventData eventData) where TEventData : IDomainEvent
        {
        }

        /// <inheritdoc/>
        public void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IDomainEvent
        {
        }

        /// <inheritdoc/>
        public void Trigger(Type eventType, IDomainEvent eventData)
        {
        }

        /// <inheritdoc/>
        public void Trigger(Type eventType, object eventSource, IDomainEvent eventData)
        {
        }

        /// <inheritdoc/>
        public Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IDomainEvent
        {
            return new Task(() => { });
        }

        /// <inheritdoc/>
        public Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IDomainEvent
        {
            return new Task(() => { });
        }

        /// <inheritdoc/>
        public Task TriggerAsync(Type eventType, IDomainEvent eventData)
        {
            return new Task(() => { });
        }

        /// <inheritdoc/>
        public Task TriggerAsync(Type eventType, object eventSource, IDomainEvent eventData)
        {
            return new Task(() => { });
        }
    }
}
