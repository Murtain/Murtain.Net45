using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.SDK.Attributes
{
    public class HttpCorrespondingAttribute : Attribute
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public HttpCorrespondingAttribute(HttpStatusCode httpStatusCode)
        {
            this.HttpStatusCode = httpStatusCode;
        }
    }
}
