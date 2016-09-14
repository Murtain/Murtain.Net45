using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Events.Handlers;

namespace Murtain.Events
{
    public class ExceptionOccursEventHandler : IEventHandler<ExceptionOccursEvent>
    {
        public ILogger Logger { get; set; }

        public ExceptionOccursEventHandler()
        {
            Logger = NullLogger.Instance;
        }
        public void HandleEvent(ExceptionOccursEvent domainEvent)
        {
            Logger.ErrorFormat("Exception handler : [{0}]-[{1}]", domainEvent.EventTime, domainEvent.Exception.Message);
        }
    }
}
