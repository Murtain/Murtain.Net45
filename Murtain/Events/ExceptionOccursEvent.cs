using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Events
{
    public class ExceptionOccursEvent : DomainEvent
    {
        public Exception Exception { get; private set; }

        public ExceptionOccursEvent(Exception exception)
        {
            Exception = exception;
        }
    }
}
