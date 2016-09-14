using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Castle.DynamicProxy;
using Murtain.Extensions;
using Murtain.Utils;
using Murtain.Runtime.Validation.Attributes;

namespace Murtain.Runtime.Validation.Interception
{
    public class ValidationInterceptor : IInterceptor
    {
        private MethodInfo method;
        private object[] parameterValues;
        private ParameterInfo[] parameters;
        private List<ValidationResult> validationErrors;

        public void Intercept(IInvocation invocation)
        {
            this.method = invocation.Method;
            this.parameterValues = invocation.Arguments;
            this.parameters = method.GetParameters();
            this.validationErrors = new List<ValidationResult>();

            this.Validate();

            invocation.Proceed();
        }
        private void Validate()
        {
            if (!method.IsPublic)
            {
                return;
            }

            if (method.IsDefined(typeof(DisableValidationAttribute)))
            {
                return;
            }

            if (parameters.IsNullOrEmpty())
            {
                return;
            }

            if (parameters.Length != parameterValues.Length)
            {
                throw new Exception("Method parameter count does not match with argument count!");
            }

            for (var i = 0; i < parameters.Length; i++)
            {
                ValidateMethodParameter(parameters[i], parameterValues[i]);
            }

            if (validationErrors.Any())
            {
                throw new ValidationException("Method arguments are not valid! See ValidationErrors for details.", validationErrors);
            }
        }
        private void ValidateMethodParameter(ParameterInfo parameterInfo, object parameterValue)
        {
            if (parameterValue == null)
            {
                if (!parameterInfo.IsOptional && !parameterInfo.IsOut && !TypeHelper.IsPrimitiveExtendedIncludingNullable(parameterInfo.ParameterType))
                {
                    validationErrors.Add(new ValidationResult(parameterInfo.Name + " is null!", new[] { parameterInfo.Name }));
                }

                return;
            }

            ValidateObjectRecursively(parameterValue);
        }
        private void ValidateObjectRecursively(object validatingObject)
        {
            if (validatingObject is IEnumerable && !(validatingObject is IQueryable))
            {
                foreach (var item in (validatingObject as IEnumerable))
                {
                    ValidateObjectRecursively(item);
                }
            }

            if (!(validatingObject is IValidate))
            {
                return;
            }

            SetValidationAttributeErrors(validatingObject);

            if (validatingObject is ICustomValidate)
            {
                (validatingObject as ICustomValidate).AddValidationErrors(validationErrors);
            }

            var properties = TypeDescriptor.GetProperties(validatingObject).Cast<PropertyDescriptor>();
            foreach (var property in properties)
            {
                ValidateObjectRecursively(property.GetValue(validatingObject));
            }
        }
        private void SetValidationAttributeErrors(object validatingObject)
        {
            var properties = TypeDescriptor.GetProperties(validatingObject).Cast<PropertyDescriptor>();
            foreach (var property in properties)
            {
                var validationAttributes = property.Attributes.OfType<ValidationAttribute>().ToArray();
                if (validationAttributes.IsNullOrEmpty())
                {
                    continue;
                }

                var validationContext = new ValidationContext(validatingObject)
                {
                    DisplayName = property.Name,
                    MemberName = property.Name
                };

                foreach (var attribute in validationAttributes)
                {
                    var result = attribute.GetValidationResult(property.GetValue(validatingObject), validationContext);
                    if (result != null)
                    {
                        validationErrors.Add(result);
                    }
                }
            }
        }
    }

}
