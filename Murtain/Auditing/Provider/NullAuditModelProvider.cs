namespace Murtain.Auditing.Provider
{
    /// <summary>
    /// Null implementation of <see cref="IAuditingModelProvider"/>.
    /// </summary>
    internal class NullAuditingModelProvider : IAuditingModelProvider
    {

        public void Fill(AuditingMessage auditInfo)
        {
            
        }
    }
}