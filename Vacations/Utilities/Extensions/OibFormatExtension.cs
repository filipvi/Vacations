using System;

namespace Vacations.Utilities.Extensions
{
    public static class OibFormatExtension
    {
        /// <summary>
        /// Check control number OIB
        /// </summary>
        /// <param name="oib"></param>
        /// <returns>bool is valid oib</returns>
        public static bool IsOib(this string oib)
        {
            if (string.IsNullOrWhiteSpace(oib) || oib.Length != 11)
            {
                return false;
            }

            // long b;
            // if (!long.TryParse(oib, out b)) return false;
            if (!long.TryParse(oib, out _)) return false;

            int a = 10;
            for (int i = 0; i < 10; i++)
            {
                a = a + Convert.ToInt32(oib.Substring(i, 1));
                a = a % 10;
                if (a == 0) a = 10;
                a *= 2;
                a = a % 11;
            }
            int kontrolni = 11 - a;
            if (kontrolni == 10) kontrolni = 0;

            return kontrolni == Convert.ToInt32(oib.Substring(10, 1));
        }
    }
}
