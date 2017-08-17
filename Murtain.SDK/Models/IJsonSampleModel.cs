using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Murtain.SDK.Models
{
    public interface IJsonSampleModel
    {
        object GetRequestSampleModel();
        object GetResponseSampleModel();
        object GetErrorSampleModel();
    }
}