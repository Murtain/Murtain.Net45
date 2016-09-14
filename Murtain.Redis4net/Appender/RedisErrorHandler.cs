using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Redis4net.Appender
{
    public class RedisErrorHandler : IErrorHandler 
    {
        public void Error(string message)
        {
        }


        public void Error(string message, Exception e)
        {
        }

        public void Error(string message, Exception e, ErrorCode errorCode)
        {
        }
    }
}
