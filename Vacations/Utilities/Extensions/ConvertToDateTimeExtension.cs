using System;
using System.Globalization;

namespace Vacations.Utilities.Extensions
{
    public static class ConvertToDateTimeExtension
    {
        /// <summary>
        /// Convert string to datetime
        /// </summary>
        /// <param name="data"> pass data in string format</param>
        public static DateTime StringToDateTime(this string data)
        {
            return DateTime.ParseExact(data, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }
    }
}