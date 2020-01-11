using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.Utils
{
    public class DateFormatter
    {

        public static bool TryParseFormattedDateTime(string input, string format = "MM/dd/yyyy h:mm tt")
        {
            if (DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime output))
            {
                return true;
            }
            return false;
        }

        public static DateTime ParseFormattedDateTime(string input, string format = "MM/dd/yyyy h:mm tt")
        {

            if (DateTime.TryParse(input, out DateTime output))
            {
                return output;
            }
            else
            {
                return DateTime.ParseExact(input, format, null);
            }

        }

        public static string ConvertToFormattedString(DateTime input, string format = "MM/dd/yyyy h:mm tt")
        {
            return input.ToString(format);
        }

    }
}