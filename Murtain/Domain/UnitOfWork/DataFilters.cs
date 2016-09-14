
namespace Murtain.Domain.UnitOfWork
{
    /// <summary>
    /// Standard filters of ABP.
    /// </summary>
    public static class DataFilters
    {
        /// <summary>
        /// "SoftDelete".
        /// Soft delete filter.
        /// Prevents getting deleted data from database.
        /// See <see cref="ISoftDelete"/> interface.
        /// </summary>
        public const string SoftDelete = "SoftDelete";
    }
}