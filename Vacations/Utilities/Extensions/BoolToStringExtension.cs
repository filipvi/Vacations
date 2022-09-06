namespace Vacations.Utilities.Extensions
{
    public static class BoolToStringExtension
    {
        /// <summary>
        /// Convert boolean flag to string (Da,Ne)
        /// </summary>
        /// <param name="data"> Method on boolean value</param>
        public static string BoolToString(this bool data)
        {
            return data ? "Da" : "Ne";
        }

        /// <summary>
        /// Convert string(Da,Ne) to boolean flag 
        /// </summary>
        /// <param name="data"> Method on string value</param>
        public static bool StringToBool(this string data)
        {
            return data.Equals("Da") ? true : false;
        }
    }
}
