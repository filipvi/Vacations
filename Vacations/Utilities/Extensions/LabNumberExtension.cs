namespace Vacations.Utilities.Extensions
{
    public static class LabNumberExtension
    {
        public static string ToShortLabNumber(this int? number, int year)
        {
            if (number.HasValue)
            {
                var yearInfo = year.ToString();

                return number + "/" + yearInfo.Substring(yearInfo.Length - 2);
            }

            return string.Empty;
        }

        public static string ToLongLabNumber(this int? number, int year)
        {
            if (number.HasValue)
            {
                var yearInfo = year.ToString();

                return number + "/" + yearInfo;
            }

            return string.Empty;
        }
    }
}
