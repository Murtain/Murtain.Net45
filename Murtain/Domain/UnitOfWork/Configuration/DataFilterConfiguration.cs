using System.Collections.Generic;

namespace Murtain.Domain.UnitOfWork.Configuration
{
    /// <summary>
    /// Used to configure unit of work data filter.
    /// </summary>
    public class DataFilterConfiguration : IDataFilterConfiguration
    {
        /// <summary>
        /// filter filed name
        /// </summary>
        public string FilterName { get; private set; }
        /// <summary>
        /// IsEnabled
        /// </summary>
        public bool IsEnabled { get; private set; }
        /// <summary>
        /// FilterParameters
        /// </summary>
        public IDictionary<string, object> FilterParameters { get; private set; }

        public DataFilterConfiguration(string filterName, bool isEnabled)
        {
            FilterName = filterName;
            IsEnabled = isEnabled;
            FilterParameters = new Dictionary<string, object>();
        }

        internal DataFilterConfiguration(DataFilterConfiguration filterToClone) 
            : this(filterToClone.FilterName, filterToClone.IsEnabled)
        {
            foreach (var filterParameter in filterToClone.FilterParameters)
            {
                FilterParameters[filterParameter.Key] = filterParameter.Value;
            }
        }
    }
}