using System;
using System.Globalization;

namespace Murtain.Localization
{
    /// <summary>
    /// A class that gets the same string on every localization.
    /// </summary>
    [Serializable]
    public class LocalizationFixedString : ILocalizationString
    {
        /// <summary>
        /// The fixed string.
        /// Whenever Localize methods called, this string is returned.
        /// </summary>
        public virtual string FixedString { get; private set; }

        /// <summary>
        /// Needed for serialization.
        /// </summary>
        private LocalizationFixedString()
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="LocalizationFixedString"/>.
        /// </summary>
        /// <param name="fixedString">
        /// The fixed string.
        /// Whenever Localize methods called, this string is returned.
        /// </param>
        public LocalizationFixedString(string fixedString)
        {
            FixedString = fixedString;
        }

        public string Localize(ILocalizationContext context)
        {
            return FixedString;
        }

        public string Localize(ILocalizationContext context, CultureInfo culture)
        {
            return FixedString;
        }

        public override string ToString()
        {
            return FixedString;
        }
    }
}