using System;
using System.Net;

namespace Murtain.Web.Attributes
{
    public class HttpStatusAttribute : Attribute
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public HttpStatusAttribute(HttpStatusCode httpStatusCode)
        {
            this.HttpStatusCode = httpStatusCode;
        }
    }
}