using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Events.Handlers
{

    internal class HandlerDispose : IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly Type _eventType;
        private readonly IEventHandler _handler;

        public HandlerDispose(IEventBus eventBus, Type eventType, IEventHandler handler)
        {
            _eventBus = eventBus;
            _eventType = eventType;
            _handler = handler;
        }

        public void Dispose()
        {
            _eventBus.Unregister(_eventType, _handler);
        }
    }
}
