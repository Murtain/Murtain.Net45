using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Murtain.SDK.Attributes
{
    public class ReturnCodeAttribute : Attribute
    {
        public ReturnCodeAttribute(Type responseCode)
        {
            ReturnCode = responseCode;
        }

        public Type ReturnCode { get; private set; }
    }
}