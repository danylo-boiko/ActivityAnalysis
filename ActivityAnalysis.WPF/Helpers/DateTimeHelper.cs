using System;
using System.Globalization;

namespace ActivityAnalysis.WPF.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetFirstDayOfWeek(this DateTime date)
        {
            DateTime firstDay = new DateTime(DateTime.Now.Year, 1, 1);
            while (firstDay.DayOfWeek != DayOfWeek.Monday)
            {
                firstDay = firstDay.AddDays(-1);
            }
            return firstDay.AddDays(7 *  ISOWeek.GetWeekOfYear(date)); 
        }

        public static bool IsDateInRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate) =>
            dateToCheck >= startDate && dateToCheck < endDate.AddDays(1);
    }
}