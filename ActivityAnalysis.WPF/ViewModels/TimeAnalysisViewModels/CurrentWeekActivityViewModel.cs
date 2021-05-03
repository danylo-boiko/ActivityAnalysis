using System;
using System.Collections.Generic;
using ActivityAnalysis.Domain.Models;
using ActivityAnalysis.WPF.Helpers;
using LiveCharts;
using LiveCharts.Wpf;

namespace ActivityAnalysis.WPF.ViewModels.TimeAnalysisViewModels
{
    public class CurrentWeekActivityViewModel : ViewModelBase
    {
        public SeriesCollection CurrentWeekActivity { get; }
        public Func<double, string> Formatter { get; set; } = value => value.SecondsToAbbreviatedString();

        public CurrentWeekActivityViewModel(ICollection<Activity> activities)
        {
            CurrentWeekActivity = GetCurrentWeekActivitySeriesCollection(activities);
        }
        
        private SeriesCollection GetCurrentWeekActivitySeriesCollection(ICollection<Activity> activities)
        {
            return new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>(GetCurrentWeekActivity(activities)),
                    LabelPoint = value => ((double)value.Instance).SecondsToFullString(),
                    Title = String.Empty
                }
            };
        }
        
        private ICollection<double> GetCurrentWeekActivity(ICollection<Activity> activities)
        {
            DateTime startCurrentWeek = DateTime.Now.GetFirstDayOfWeek();
            DateTime stopCurrentWeek = DateTime.Now.GetFirstDayOfWeek().AddDays(6);

            Dictionary<int, double> resultTimes = new Dictionary<int, double>();
            
            for (int i = 0; i < 7; i++)
            {
                resultTimes.Add(i, 0.0);
            }
                
            foreach (Activity activity in activities)
            {
                DateTime dateTime = DateTime.Parse(activity.DayOfUse);
                if (dateTime.IsDateInRange(startCurrentWeek, stopCurrentWeek))
                {
                    int index = (int) dateTime.DayOfWeek == 0 ? 6 : (int) dateTime.DayOfWeek - 1;
                    resultTimes[index] += TimeSpan.FromTicks(activity.TimeSpent.Ticks).TotalSeconds;
                }
            }

            return resultTimes.Values;
        }
    }
}