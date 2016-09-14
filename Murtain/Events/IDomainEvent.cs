using System;

namespace Murtain.Events
{
    /// <summary>
    /// Defines interface for all Event data classes.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// The time when the event occured.
        /// </summary>
        DateTime EventTime { get; set; }

        /// <summary>
        /// The object which triggers the event (optional).
        /// </summary>
        object EventSource { get; set; }
    }
}
