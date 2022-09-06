using System;
using System.Globalization;

namespace Vacations.Utilities.Extensions
{
    public static class DateFormatExtension
    {
        /// <summary>
        /// Check if given datetime in string is valid date
        /// </summary>
        public static bool IsDate(this string textDate)
        {
            string dateFormat = "dd.MM.yyyy";

            bool isDate = DateTime.TryParseExact(textDate, dateFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out _);

            return isDate;
        }
    }
}
