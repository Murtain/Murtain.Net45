using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Transactions;

using Castle.Core.Logging;
using Castle.DynamicProxy;

using Murtain.Extensions;
using Murtain.Domain.UnitOfWork;
using Murtain.Runtime.Session;
using Murtain.Threading;
using Murtain.Auditing.Store;
using Murtain.Auditing.Configuration;
using Murtain.Auditing.Provider;
using Murtain.Auditing.Extensions;

namespace Murtain.Auditing.ConventionalRegistras
{
    public class AuditingInterceptor : IInterceptor
    {
        public IAppSession AppSession { get; set; }
        public ILogger Logger { get; set; }
        public IAuditingStore AuditingStore { get; set; }

        private readonly IAuditingConfiguration _configuration;
        private readonly IAuditingModelProvider _auditInfoProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AuditingInterceptor(IAuditingConfiguration configuration, IAuditingModelProvider auditInfoProvider, IUnitOfWorkManager unitOfWorkManager, IAuditingStore auditingStore)
        {
            _configuration = configuration;
            _auditInfoProvider = auditInfoProvider;
            _unitOfWorkManager = unitOfWorkManager;

            AppSession = NullAppSession.Instance;
            Logger = NullLogger.Instance;
            AuditingStore = auditingStore;
        }

        public void Intercept(IInvocation invocation)
        {
            if (!AuditingHelper.ShouldSaveAudit(invocation.MethodInvocationTarget, _configuration, AppSession))
            {
                invocation.Proceed();
                return;
            }

            var auditInfo = CreateAuditInfo(invocation);

            if (AsyncHelper.IsAsyncMethod(invocation.Method))
            {
                PerformAsyncAuditing(invocation, auditInfo);
            }
            else
            {
                PerformSyncAuditing(invocation, auditInfo);
            }

        }

        private AuditingMessage CreateAuditInfo(IInvocation invocation)
        {
            var auditInfo = new AuditingMessage
            {
                InputParameters = ConvertArgumentsToJson(invocation),
                Time = DateTime.Now,
            };

            _auditInfoProvider.Fill(auditInfo);

            return auditInfo;
        }

        private void PerformSyncAuditing(IInvocation invocation, AuditingMessage auditInfo)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex;
                throw;
            }
            finally
            {
                stopwatch.Stop();

                auditInfo.Output = invocation.ReturnValue.ToJsonString();
                auditInfo.Duration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                AuditingStore.Save(auditInfo);
            }
        }
        private void PerformAsyncAuditing(IInvocation invocation, AuditingMessage auditInfo)
        {
            var stopwatch = Stopwatch.StartNew();

            invocation.Proceed();

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithFinally(
                    (Task)invocation.ReturnValue,
                    exception => SaveAuditInfo(auditInfo, exception)
                    );
            }
            else
            {
                invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    exception => SaveAuditInfo(auditInfo, exception)
                    );
            }

            stopwatch.Stop();

            if (auditInfo.Exception == null)
            {
                auditInfo.Output = invocation.ReturnValue.ToJsonString();
            }
            auditInfo.Duration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
            AuditingStore.Save(auditInfo);

        }

        private string ConvertArgumentsToJson(IInvocation invocation)
        {
            try
            {
                var parameters = invocation.MethodInvocationTarget.GetParameters();
                if (parameters.IsNullOrEmpty())
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();
                for (int i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    var argument = invocation.Arguments[i];
                    dictionary[parameter.Name] = argument;
                }

                return dictionary.ToJsonString();
            }
            catch (Exception ex)
            {
                Logger.Warn("Could not serialize arguments for method: " + invocation.MethodInvocationTarget.Name);
                Logger.Warn(ex.ToString(), ex);
                return "{}";
            }
        }

        private void SaveAuditInfo(AuditingMessage auditInfo, Exception exception)
        {
            auditInfo.Exception = exception;
        }
    }
}