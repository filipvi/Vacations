using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Nager.Date;
using Nager.Date.Extensions;

namespace Vacations.Utilities.Helpers;

public static class DateHelper
{
    public static async Task<int> PrepareDurationWorkingDays(DateTime startDate, DateTime endDate)
    {
        int duration = 0;

        foreach (DateTime day in EachDay(startDate, endDate))
        {
            if (day.IsWeekend(CountryCode.HR))
            {
                continue;
            }

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://date.nager.at/api/v3/IsTodayPublicHoliday/hr");
            if (response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NoContent:
                        // not holiday
                        duration++;
                        break;
                    case HttpStatusCode.NotFound:
                        // code not valid
                        break;
                    case HttpStatusCode.OK:
                        // holiday
                        break;
                }
            }
        }

        return duration;
    }

    public static int PrepareDurationInDays(DateTime startDate, DateTime endDate)
    {
        int duration = 0;

        foreach (DateTime day in EachDay(startDate, endDate))
        {
            duration++;
        }

        return duration;
    }

    private static IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
    {
        for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            yield return day;
    }
}