using System;
using System.Collections.Generic;
using System.Transactions;

namespace Murtain.Domain.UnitOfWork.Configuration
{
    /// <summary>
    /// Used to configure unit of work data filter.
    /// </summary>
    public interface IDataFilterConfiguration
    {
        /// <summary>
        /// filter filed name
        /// </summary>
        string FilterName { get; }
        /// <summary>
        /// IsEnabled
        /// </summary>
        bool IsEnabled { get; }
        /// <summary>
        /// FilterParameters
        /// </summary>
        IDictionary<string, object> FilterParameters { get; }
    }
}