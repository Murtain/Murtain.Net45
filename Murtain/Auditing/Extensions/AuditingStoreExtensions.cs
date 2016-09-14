using Murtain.Auditing.Store;
using Murtain.Threading;

namespace Murtain.Auditing.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IAuditingStore"/>.
    /// </summary>
    public static class AuditingStoreExtensions
    {
        /// <summary>
        /// Should save audits to a persistent store.
        /// </summary>
        /// <param name="auditingStore">Auditing store</param>
        /// <param name="auditInfo">Audit informations</param>
        public static void Save(this IAuditingStore auditingStore, AuditingMessage auditInfo)
        {
            AsyncHelper.RunSync(() => auditingStore.SaveAsync(auditInfo));
        }
    }
}