using System;
using System.Text;

namespace Murtain.Auditing
{
    /// <summary>
    /// This informations are collected for an <see cref="AuditedAttribute"/> method.
    /// </summary>
    public class AuditingMessage
    {
        public AuditingMessage()
        {
        }

        public AuditingMessage(Exception exception)
        {
            this.Exception = exception;
        }
        public AuditingMessage(string output, Exception exception)
        {
            this.Output = output;
            this.Exception = exception;
        }
        /// <summary>
        /// output.
        /// </summary>
        public virtual string Output { get; set; }

        /// <summary>
        /// Calling parameters.
        /// </summary>
        public virtual string InputParameters { get; set; }

        /// <summary>
        /// Start time of the method execution.
        /// </summary>
        public virtual DateTime Time { get; set; }

        /// <summary>
        /// Total duration of the method call.
        /// </summary>
        public virtual int Duration { get; set; }

        /// <summary>
        /// IP address of the client.
        /// </summary>
        public virtual string Route { get; set; }

        /// <summary>
        /// Name (generally computer name) of the client.
        /// </summary>
        public virtual string Host { get; set; }

        /// <summary>
        /// Browser information if this method is called in a web request.
        /// </summary>
        public virtual string UserAgent { get; set; }
        /// <summary>
        /// User Data
        /// </summary>
        public virtual string UserData { get; set; }

        /// <summary>
        /// Exception object, if an exception occured during execution of the method.
        /// </summary>
        public Exception Exception { get; set; }

    }
}