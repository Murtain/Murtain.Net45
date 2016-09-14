using Murtain.Dependency;
namespace Murtain.EntityFramework.Configuration
{
    /// <summary>
    /// Used to configure EntityFramework.
    /// </summary>
    public interface IEntityFrameworkConfiguration
    {
        /// <summary>
        /// Gets/sets default connection string used by ORM module.
        /// It can be name of a connection string in application's config file or can be full connection string.
        /// </summary>
        string DefaultNameOrConnectionString { get; set; }
    }
}