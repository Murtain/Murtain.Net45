using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using Murtain.Extensions;

namespace Murtain.Web.Exceptions
{

    public class UserFriendlyException : Exception
    {
        public Enum Code { get; set; }

        public UserFriendlyException(Enum code) :
            base(code.TryDescription())
        {
            this.Code = code;
        }
        public UserFriendlyException(Enum code, string message)
            : base(message)
        {
            this.Code = code;
        }
    }
}
