namespace Murtain.EntityFramework.Configuration
{
    /// <summary>
    /// Used to configure EntityFramework.
    /// </summary>
    public class EntityFrameworkConfiguration : IEntityFrameworkConfiguration
    {
        /// <summary>
        /// Gets/sets default connection string used by ORM module.
        /// It can be name of a connection string in application's config file or can be full connection string.
        /// </summary>
        public string DefaultNameOrConnectionString { get; set; }

        public EntityFrameworkConfiguration()
        {
            DefaultNameOrConnectionString = "Default";
        }
    }
}