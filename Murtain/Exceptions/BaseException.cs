using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Exceptions
{
    /// <summary>
    /// Base exception type for those are thrown by Abp system for Abp specific exceptions.
    /// </summary>
    [Serializable]
    public class BaseException : System.Exception
    {
        /// <summary>
        /// Creates a new <see cref="BaseException"/> object.
        /// </summary>
        public BaseException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="BaseException"/> object.
        /// </summary>
        public BaseException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="BaseException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public BaseException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="BaseException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public BaseException(string message, System.Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
