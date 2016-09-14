namespace Murtain.Auditing.Configuration
{
    internal class AuditingConfiguration : IAuditingConfiguration
    {
        public bool IsEnabled { get; set; }

        public bool IsEnableControllers { get; private set; }

        public IAuditingSelectorList Selectors { get; private set; }

        public AuditingConfiguration()
        {
            IsEnabled = true;
            Selectors = new AuditingSelectorList();
        }
    }
}