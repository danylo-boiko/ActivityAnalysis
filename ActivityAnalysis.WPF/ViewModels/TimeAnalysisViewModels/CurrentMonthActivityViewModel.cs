using System;
using System.Collections.Generic;
using System.Globalization;
using ActivityAnalysis.Domain.Models;
using ActivityAnalysis.WPF.Helpers;
using LiveCharts;
using LiveCharts.Wpf;

namespace ActivityAnalysis.WPF.ViewModels.TimeAnalysisViewModels
{
    public class CurrentMonthActivityViewModel : ViewModelBase
    {
        public SeriesCollection CurrentMonthActivity { get; }
        public Func<double, string> Formatter { get; set; } = value => value.SecondsToAbbreviatedString();
        
        public CurrentMonthActivityViewModel(ICollection<Activity> activities)
        {
            CurrentMonthActivity = GetCurrentMonthActivitySeriesCollection(activities);
        }
        
        private SeriesCollection GetCurrentMonthActivitySeriesCollection(ICollection<Activity> activities)
        {
            return new SeriesCollection {
                new ColumnSeries
                {
                    Values = new ChartValues<double>(GetCurrentMonthActivity(activities)),
                    MaxColumnWidth = 10,
                    LabelPoint = value =>((double) value.Instance).SecondsToFullString(),
                    Title = String.Empty
                }
            };
        }
        
        private  ICollection<double> GetCurrentMonthActivity(ICollection<Activity> activities)
        {
            var startCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endCurrentMonth = startCurrentMonth.AddMonths(1).AddDays(-1);
            int startCurrentMonthWeek = ISOWeek.GetWeekOfYear(startCurrentMonth);
            int endCurrentMonthWeek = ISOWeek.GetWeekOfYear(endCurrentMonth);

            Dictionary<int, double> resultTimes = new Dictionary<int, double>();

            for (int i = startCurrentMonthWeek; i <= endCurrentMonthWeek; i++)
            {
                resultTimes.Add(i,0.0);
            }
            
            foreach (Activity activity in activities)
            {
                int activityWeek = ISOWeek.GetWeekOfYear(DateTime.Parse(activity.DayOfUse));
                if (startCurrentMonthWeek <= activityWeek && activityWeek <= endCurrentMonthWeek)
                {
                    resultTimes[activityWeek] += TimeSpan.FromTicks(activity.TimeSpent.Ticks).TotalSeconds;
                }
            }
            
            return resultTimes.Values;
        }
    }
}