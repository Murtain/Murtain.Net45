using System.Linq;
using System.Reflection;

using Murtain.Runtime.Session;
using Murtain.Auditing.Configuration;
using Murtain.Auditing.Attributes;
using Murtain.Domain.Services;

namespace Murtain.Auditing
{
    public static class AuditingHelper
    {
        public static bool ShouldSaveAudit(MethodInfo methodInfo, IAuditingConfiguration configuration, IAppSession appSession, bool defaultValue = false)
        {
            if ((configuration == null || !configuration.IsEnabled)
                || (methodInfo == null)
                || (!methodInfo.IsPublic)
                || (methodInfo.IsDefined(typeof(DisableAuditingAttribute)))
                || (methodInfo.DeclaringType != null && methodInfo.DeclaringType.IsDefined(typeof(DisableAuditingAttribute)))
                )
            {
                return false;
            }

            if ((methodInfo.IsDefined(typeof(AuditedAttribute)))
                || (methodInfo.DeclaringType != null && methodInfo.DeclaringType.IsDefined(typeof(AuditedAttribute)))
                || (methodInfo.DeclaringType != null &&configuration.Selectors.Any(selector => selector.Predicate(methodInfo.DeclaringType)))
                || (typeof(IApplicationService).IsAssignableFrom(methodInfo.DeclaringType) && methodInfo.DeclaringType != typeof(IApplicationService) && !methodInfo.DeclaringType.IsAbstract)
                )
            {
                return true;
            }
            return defaultValue;
        }
    }
}