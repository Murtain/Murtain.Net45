using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Runtime.Validation.Attributes;

namespace Murtain.Runtime.Validation
{
    [Serializable]
    [Validator("BOOLEAN")]
    public class ValidatorBoolean : ValidatorBase
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (value is bool)
            {
                return true;
            }

            bool b;
            return bool.TryParse(value.ToString(), out b);
        }
    }
}
