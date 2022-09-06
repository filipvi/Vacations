using System;

namespace Vacations.Utilities.Extensions
{
    public static class ConvertToStringExtension
    {
        /// <summary>
        /// Nullable decimal to string, specify format (decimal spaces)
        /// </summary>
        public static string DecimalToString(this decimal? data, string format)
        {
            if (!data.HasValue) return string.Empty;

            return data.Value.ToString(format);
        }

        /// <summary>
        /// Decimal to string, specify format (decimal spaces)
        /// </summary>
        public static string DecimalToString(this decimal data, string format)
        {
            return data.ToString(format);
        }

        /// <summary>
        /// Nullable dateTime to string
        /// </summary>
        public static string DateToString(this DateTime? data)
        {
            if (!data.HasValue) return string.Empty;

            string format = "dd.MM.yyyy";

            return data.Value.ToString(format);
        }

        /// <summary>
        /// DateTime to string
        /// </summary>
        public static string DateToString(this DateTime data)
        {
            string format = "dd.MM.yyyy";

            return data.ToString(format);
        }

        /// <summary>
        /// Nullable int to string
        /// </summary>
        public static string NumberToString(this int? data)
        {
            if (!data.HasValue) return string.Empty;

            return data.Value.ToString();
        }

        public static string GetLastChars(this int data, int numberOfChars)
        {
            var stringYear = data.ToString();

            if (numberOfChars >= stringYear.Length)
            {
                return stringYear;
            }

            return stringYear.Substring(stringYear.Length - numberOfChars);
        }

        public static string ToShortYear(this int year)
        {
            var yearInfo = year.ToString();

            if (yearInfo.Length > 2)
            {
                return yearInfo.Substring(yearInfo.Length - 2);
            }

            return yearInfo;
        }

        public static string ToLongYear(this string year)
        {
            var yearInfo = year.ToString();
            var currentYear = DateTime.Now.Year;
            string currentYearFirstTwoChars = currentYear.ToString().Substring(0, 2);

            return currentYearFirstTwoChars + yearInfo;
        }


        public static string FormatDecimalValue(decimal? decimalValue, int? precision)
        {
            var formattedValue = string.Empty;

            if (!decimalValue.HasValue)
            {
                return formattedValue;
            }

            string formatExpression = "F";

            if (precision.HasValue)
            {
                formatExpression += precision.Value;
                formattedValue = decimalValue.Value.DecimalToString(formatExpression);
            }
            else
            {
                formattedValue = decimalValue.DecimalToString(formatExpression);
            }

            return formattedValue;
        }

        public static string PrepareForSearch(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            return value.Trim().ToLower();
        }
        
        public static string TrimValue(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            return value.Trim();
        }
    }
}
