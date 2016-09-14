using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Murtain.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Boolean"/>.
    /// </summary>
    public static class BooleanExtensions
    {
        public static string ToLower(this bool value)
        {
            return value.ToString().ToLower();
        }
    }
}