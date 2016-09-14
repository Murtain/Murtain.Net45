using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Web.Models
{
    public class ErrorViewModel 
    {
        public ErrorInfo ErrorInfo { get; set; }

        public Exception Exception { get; set; }

        public ErrorViewModel()
        {

        }

        public ErrorViewModel(Exception exception, ErrorInfo errorInfo)
        {
            Exception = exception;
            ErrorInfo = errorInfo;
        }
    }
}
