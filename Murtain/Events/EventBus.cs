using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Murtain.Events.Handlers;
using Murtain.Dependency;

namespace Murtain.Events
{
    /// <summary>
    /// Implements EventBus as Singleton pattern.
    /// </summary>
    public class EventBus : IEventBus, ISingletonDependency
    {
        /// <summary>
        /// Reference to the Logger.
        /// </summary>
        public ILogger Logger { get; set; }

        private readonly Dictionary<Type, List<IEventHandler>> _handlers;
        private readonly IIocManager _iocManager;
        /// <summary>
        /// Creates a new <see cref="EventBus"/> instance.
        /// Instead of creating a new instace, you can use <see cref="Default"/> to use Global <see cref="EventBus"/>.
        /// </summary>
        public EventBus()
        {
            _handlers = new Dictionary<Type, List<IEventHandler>>();
            _iocManager = IocManager.Container;
            Logger = NullLogger.Instance;
        }

        /// <inheritdoc/>
        public IDisposable Register<TDomainEvent>(Action<TDomainEvent> action) where TDomainEvent : IDomainEvent
        {
            return Register(typeof(TDomainEvent), new ActionEventHandler<TDomainEvent>(action));
        }

        /// <inheritdoc/>
        public IDisposable Register<TDomainEvent>(IEventHandler<TDomainEvent> handler) where TDomainEvent : IDomainEvent
        {
            return Register(typeof(TDomainEvent), handler);
        }

        /// <inheritdoc/>
        public IDisposable Register<TEventData, THandler>()
            where TEventData : IDomainEvent
            where THandler : IEventHandler<TEventData>, new()
        {
            return Register(typeof(TEventData), (IEventHandler)_iocManager.Resolve(typeof(THandler)));
        }

        /// <inheritdoc/>
        public IDisposable Register(Type eventType, IEventHandler handler)
        {
            lock (_handlers)
            {
                GetOrCreateHandlers(eventType).Add(handler);
                return new HandlerDispose(this, eventType, handler);
            }
        }

        private List<IEventHandler> GetOrCreateHandlers(Type eventType)
        {
            List<IEventHandler> handlers;
            if (!_handlers.TryGetValue(eventType, out handlers))
            {
                _handlers[eventType] = handlers = new List<IEventHandler>();
            }

            return handlers;
        }

        /// <inheritdoc/>
        public void Unregister<TDomainEvent>(Action<TDomainEvent> action) where TDomainEvent : IDomainEvent
        {
            Unregister(typeof(TDomainEvent), new ActionEventHandler<TDomainEvent>(action));
        }

        /// <inheritdoc/>
        public void Unregister<TDomainEvent>(IEventHandler<TDomainEvent> handler) where TDomainEvent : IDomainEvent
        {
            Unregister(typeof(TDomainEvent), handler);
        }

        /// <inheritdoc/>
        public void Unregister(Type eventType, IEventHandler handler)
        {
            GetOrCreateHandlers(eventType).Remove(handler);
        }

        public void UnregisterAll<TDomainEvent>() where TDomainEvent : IDomainEvent
        {
            UnregisterAll(typeof(TDomainEvent));
        }

        /// <inheritdoc/>
        public void UnregisterAll(Type eventType)
        {
            lock (_handlers)
            {
                GetOrCreateHandlers(eventType).Clear();
            }
        }

        /// <inheritdoc/>
        public void Trigger<TEventData>(TEventData eventData) where TEventData : IDomainEvent
        {
            Trigger((object)null, eventData);
        }

        /// <inheritdoc/>
        public void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IDomainEvent
        {
            Trigger(typeof(TEventData), eventSource, eventData);
        }

        /// <inheritdoc/>
        public void Trigger(Type eventType, IDomainEvent eventData)
        {
            Trigger(eventType, null, eventData);
        }

        /// <inheritdoc/>
        public void Trigger(Type eventType, object eventSource, IDomainEvent domainEvent)
        {

            domainEvent.EventSource = eventSource;

            foreach (var eventHandler in GetOrCreateHandlers(eventType))
            {
                if (eventHandler == null)
                {
                    throw new Exception("Registered event handler for event type " + eventType.Name + " does not implement IEventHandler<" + eventType.Name + "> interface!");
                }

                var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

                try
                {
                    handlerType
                        .GetMethod("HandleEvent", BindingFlags.Public | BindingFlags.Instance, null, new[] { eventType }, null)
                        .Invoke(eventHandler, new object[] { domainEvent });
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            //Implements generic argument inheritance. See IEventDataWithInheritableGenericArgument
            if (eventType.IsGenericType &&
                eventType.GetGenericArguments().Length == 1 &&
                typeof(IEventDataWithInheritableGenericArgument).IsAssignableFrom(eventType))
            {
                var genericArg = eventType.GetGenericArguments()[0];
                var baseArg = genericArg.BaseType;
                if (baseArg != null)
                {
                    var baseEventType = eventType.GetGenericTypeDefinition().MakeGenericType(genericArg.BaseType);
                    var constructorArgs = ((IEventDataWithInheritableGenericArgument)domainEvent).GetConstructorArgs();
                    var baseEventData = (IDomainEvent)Activator.CreateInstance(baseEventType, constructorArgs);
                    baseEventData.EventTime = domainEvent.EventTime;
                    Trigger(baseEventType, domainEvent.EventSource, baseEventData);
                }
            }
        }

        /// <inheritdoc/>
        public Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IDomainEvent
        {
            return TriggerAsync((object)null, eventData);
        }

        /// <inheritdoc/>
        public Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IDomainEvent
        {
            ExecutionContext.SuppressFlow();

            var task = Task.Factory.StartNew(
                () =>
                {
                    try
                    {
                        Trigger(eventSource, eventData);
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn(ex.ToString(), ex);
                    }
                });

            ExecutionContext.RestoreFlow();

            return task;
        }

        /// <inheritdoc/>
        public Task TriggerAsync(Type eventType, IDomainEvent eventData)
        {
            return TriggerAsync(eventType, null, eventData);
        }

        /// <inheritdoc/>
        public Task TriggerAsync(Type eventType, object eventSource, IDomainEvent eventData)
        {
            ExecutionContext.SuppressFlow();

            var task = Task.Factory.StartNew(
                () =>
                {
                    try
                    {
                        Trigger(eventType, eventSource, eventData);
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn(ex.ToString(), ex);
                    }
                });

            ExecutionContext.RestoreFlow();

            return task;
        }


    }
}