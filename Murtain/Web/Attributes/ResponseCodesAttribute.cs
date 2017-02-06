using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Murtain.Web.Attributes
{
    public class ResponseCodeAttribute : Attribute
    {
        public ResponseCodeAttribute(Type responseCode)
        {
            ResponseCode = responseCode;
        }

        public Type ResponseCode { get; private set; }
    }
}