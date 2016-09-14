using System;

namespace Murtain.Events
{
    /// <summary>
    /// Implements <see cref="IDomainEvent"/> and provides a base for event data classes.
    /// </summary>
    [Serializable]
    public abstract class DomainEvent : IDomainEvent
    {
        /// <summary>
        /// The time when the event occured.
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// The object which triggers the event (optional).
        /// </summary>
        public object EventSource { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected DomainEvent()
        {
            EventTime = DateTime.Now;
        }
    }
}