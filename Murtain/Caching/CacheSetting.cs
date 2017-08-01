using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Caching
{
    public class CacheSetting
    {
        /// <summary>
        /// Cache name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Cache key
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Cache key group name
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// Cache key group description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Cache key group expired time
        /// </summary>
        public TimeSpan? ExpiredTime { get; set; }
    }
}
