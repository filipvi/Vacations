
namespace Vacations.Utilities.Extensions
{
    public static class ConvertToBoolExtension
    {
        /// <summary>
        /// Convert string to boolean(Da)
        /// Used for creating inquiry from excel
        /// </summary>
        /// <param name="data"> Method on boolean value</param>
        public static bool ToBool(this string data)
        {
            data = data.Trim().ToLower();
            bool value = data.Equals("da");

            return value;
        }
    }
}
