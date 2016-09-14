using System;

namespace Murtain.GlobalSettings.Models
{
    /// <summary>
    /// Defines scope of a setting.
    /// </summary>
    [Flags]
    public enum GlobalSettingScope
    {
        /// <summary>
        /// Represents a setting that can be configured/changed for the application level.
        /// </summary>
        Application = 1,
    }
}