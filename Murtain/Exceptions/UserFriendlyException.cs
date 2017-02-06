using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Murtain.Extensions;

namespace Murtain.Exceptions
{

    public class UserFriendlyExceprion : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public Enum Code { get; set; }

        public UserFriendlyExceprion(Enum code) :
            base(code.TryDescription())
        {
            this.HttpStatusCode = code.TryHttpStatusCode();
            this.Code = code;
        }
        public UserFriendlyExceprion(Enum code, string message)
            : base(message)
        {
            this.HttpStatusCode = code.TryHttpStatusCode();
            this.Code = code;
        }
    }
}
