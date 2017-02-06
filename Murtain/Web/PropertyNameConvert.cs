using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Web
{
    public static class PropertyNameConvert
    {
        public static string PasicalPropertyName(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return propertyName;
            }

            if (char.IsUpper(propertyName[0]))
            {
                return propertyName;
            }
            string pasical = char.ToUpper(propertyName[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            for (int i = 1; i < propertyName.Length; i++)
            {
                var isLast = (i == propertyName.Length - 1);

                if (propertyName[i] == '_')
                {
                    pasical += char.ToUpper(propertyName[i + 1], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
                    i++;
                    continue;
                }
                pasical += char.ToLower(propertyName[i], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            }
            return pasical;
        }
        public static string SnakeCasePropertyName(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var buffer = "";

            for (var i = 0; i < input.Length; i++)
            {
                var isLast = (i == input.Length - 1);
                var isSecondFromLast = (i == input.Length - 2);

                var curr = input[i];
                var next = !isLast ? input[i + 1] : '\0';
                var afterNext = !isSecondFromLast && !isLast ? input[i + 2] : '\0';

                buffer += char.ToLower(curr);

                if (!char.IsDigit(curr) && char.IsUpper(next))
                {
                    if (char.IsUpper(curr))
                    {
                        if (!isLast && !isSecondFromLast && !char.IsUpper(afterNext))
                            buffer += "_";
                    }
                    else
                        buffer += "_";
                }

                if (!char.IsDigit(curr) && char.IsDigit(next))
                    buffer += "_";
                if (char.IsDigit(curr) && !char.IsDigit(next) && !isLast)
                    buffer += "_";
            }

            return buffer;
        }
    }
}
