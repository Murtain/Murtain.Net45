using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Murtain.Runtime.Validation.Attributes;
using Murtain.Extensions;

namespace Murtain.Runtime.Validation
{
    [Serializable]
    public abstract class ValidatorBase : IValidator
    {
        public virtual string Name
        {
            get
            {
                var type = GetType();
                if (type.IsDefined(typeof(ValidatorAttribute)))
                {
                    return type.GetCustomAttributes(typeof(ValidatorAttribute)).Cast<ValidatorAttribute>().First().Name;
                }
                return type.Name;
            }
        }

        public object this[string key]
        {
            get { return Attributes.GetOrDefault(key); }
            set { Attributes[key] = value; }
        }

        public IDictionary<string, object> Attributes { get; private set; }

        public abstract bool IsValid(object value);

        protected ValidatorBase()
        {
            Attributes = new Dictionary<string, object>();
        }
    }
}
