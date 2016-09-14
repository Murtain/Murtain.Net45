using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Extensions;
using Murtain.Runtime.Validation.Attributes;

namespace Murtain.Runtime.Validation
{
    [Serializable]
    [Validator("NUMERIC")]
    public class ValidatorNumeric : ValidatorBase
    {
        public int MinValue
        {
            get { return (this["MinValue"] ?? "0").To<int>(); }
            set { this["MinValue"] = value; }
        }

        public int MaxValue
        {
            get { return (this["MaxValue"] ?? "0").To<int>(); }
            set { this["MaxValue"] = value; }
        }

        public ValidatorNumeric()
        {

        }

        public ValidatorNumeric(int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (value is int)
            {
                return IsValidInternal((int)value);
            }

            if (value is string)
            {
                int intValue;
                if (int.TryParse(value as string, out intValue))
                {
                    return IsValidInternal(intValue);
                }
            }

            return false;
        }

        protected virtual bool IsValidInternal(int value)
        {
            return value.IsBetween(MinValue, MaxValue);
        }
    }
}
