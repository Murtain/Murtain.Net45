using Murtain.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Web.Models
{
    public class ValidateModelResponseContentModel : ResponseContentModel
    {

        public ValidateModelResponseContentModel(Enum code, string request)
            : base(code, request)
        {
            this.ModelState = new Dictionary<string, string>();
        }
        public ValidateModelResponseContentModel(Enum code, string request, IDictionary<string, string> modelState)
        : base(code, request)
        {
            this.ModelState = modelState;
        }

        public ValidateModelResponseContentModel(Enum code, string message, string request)
            : base(code, message, request)
        {
            this.ModelState = new Dictionary<string, string>();
        }
        public ValidateModelResponseContentModel(Enum code, string message, string request, IDictionary<string, string> modelState)
          : base(code, message, request)
        {
            this.ModelState = modelState;
        }
        public IDictionary<string, string> ModelState { get; set; }

    }
}
