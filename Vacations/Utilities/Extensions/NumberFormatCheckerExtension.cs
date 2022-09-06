

namespace Vacations.Utilities.Extensions
{
    public static class NumberFormatCheckerExtension
    {
        /// <summary>
        /// Check if string is valid integer
        /// </summary>
        /// <param name="textNumber"></param>
        /// <returns>Bool is integer</returns>
        public static bool IsInteger(this string textNumber)
        {
            bool isInteger = int.TryParse(textNumber, out _);

            return isInteger;
        }

        /// <summary>
        /// Check if string is decimal number
        /// </summary>
        /// <param name="textNumber"></param>
        /// <returns>bool is decimal</returns>
        public static bool IsDecimal(this string textNumber)
        {
            bool isDecimal = decimal.TryParse(textNumber, out _);

            return isDecimal;
        }
    }
}
