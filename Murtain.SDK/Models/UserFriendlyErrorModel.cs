using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.SDK.Models
{
    public class UserFriendlyErrorModel
    {
        public UserFriendlyErrorModel(Enum code, string message)
        {
            Code = code;
            Message = message;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public Enum Code { get; set; }

        public string Message { get; set; }

    }
}
