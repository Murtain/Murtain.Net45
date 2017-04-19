using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Murtain.Extensions;
using Murtain.Web.ContractResolver;
using Murtain.Web.Extensions;

namespace Murtain.Web.Models
{
    public class ResponseContentModel
    {
        private static JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new SnakeCaseContractResolver() };

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

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None, jsonSerializerSettings);
        }

    }
}
