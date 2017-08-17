using Murtain.SDK.Extensions;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Murtain.SDK.Models
{
    public class ResponseContentModel
    {
        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; }
        public ResponseContentModel(Enum code, string request)
        {
            this.Request = request;
            this.Error = new UserFriendlyErrorModel(code, code.TryDescription());
            this.HttpStatusCode = code.TryHttpStatusCode();
        }
        public ResponseContentModel(Enum code, string message, string request)
        {
            this.Request = request;
            this.Error = new UserFriendlyErrorModel(code, message);
            this.HttpStatusCode = code.TryHttpStatusCode();
        }

        public UserFriendlyErrorModel Error { get; set; }
        public string Request { get; set; }

    }
}
