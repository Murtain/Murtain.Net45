using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Runtime.Validation
{

    [Serializable]
    public class ValidationException : Exception
    {
        public List<ValidationResult> ValidationErrors { get; set; }

        public ValidationException()
        {
            ValidationErrors = new List<ValidationResult>();
        }
        public ValidationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
            ValidationErrors = new List<ValidationResult>();
        }
        public ValidationException(string message)
            : base(message)
        {
            ValidationErrors = new List<ValidationResult>();
        }
        public ValidationException(string message, List<ValidationResult> validationErrors)
            : base(message)
        {
            ValidationErrors = validationErrors;
        }
        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            ValidationErrors = new List<ValidationResult>();
        }
    }
}
