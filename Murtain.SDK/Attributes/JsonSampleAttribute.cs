using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Murtain.SDK.Attributes
{
    public class JsonSampleAttribute : Attribute
    {
        public JsonSampleAttribute(Type sampleType)
        {
            SampleType = sampleType;
        }

        public Type SampleType { get; set; }
    }
}