using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Runtime.Validation.Attributes;

namespace Murtain.Runtime.Validation
{
    [Validator("NULL")]
    [Serializable]
    public class ValidatorAlwaysValid : ValidatorBase
    {
        public override bool IsValid(object value)
        {
            return true;
        }
    }
}
