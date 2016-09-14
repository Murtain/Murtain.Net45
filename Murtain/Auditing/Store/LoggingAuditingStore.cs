using System.Threading.Tasks;
using Castle.Core.Logging;
using Newtonsoft.Json;

namespace Murtain.Auditing.Store
{
    /// <summary>
    /// Implements <see cref="IAuditingStore"/> to simply write audits to logs.
    /// </summary>
    public class LoggingAuditingStore : IAuditingStore
    {
        public ILogger Logger { get; set; }

        public LoggingAuditingStore()
        {
            Logger = NullLogger.Instance;
        }

        public Task SaveAsync(AuditingMessage auditInfo)
        {
            Logger.Info(JsonConvert.SerializeObject(auditInfo));
            return Task.FromResult(0);
        }
    }
}