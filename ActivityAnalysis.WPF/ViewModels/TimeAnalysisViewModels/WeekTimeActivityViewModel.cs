using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using ActivityAnalysis.Domain.Models;
using ActivityAnalysis.WPF.Helpers;

namespace ActivityAnalysis.WPF.ViewModels.TimeAnalysisViewModels
{
    public class WeekTimeActivityViewModel : ViewModelBase
    {
        public string WeekActivity { get; }
        public string LastWeekActivityPercent { get; }
        public Brush LastWeekActivityPercentColor { get; }

        public WeekTimeActivityViewModel(ICollection<Activity> activities)
        {
            TimeSpan thisWeekTime = ActivityPerWeek(DateTime.Now, activities);
            TimeSpan lastWeekTime = ActivityPerWeek(DateTime.Now.AddDays(-7), activities);
            double percent = thisWeekTime / lastWeekTime * 100;
            double lastWeekPercent = thisWeekTime == TimeSpan.Zero || lastWeekTime == TimeSpan.Zero ? 100 : percent;

            WeekActivity = thisWeekTime.SecondsToFullString();
            LastWeekActivityPercent = $"{Math.Round(lastWeekPercent, 1)} %";
            LastWeekActivityPercentColor = thisWeekTime >= lastWeekTime ? Brushes.Green : Brushes.Red;
        }
        
        private  TimeSpan ActivityPerWeek(DateTime date, ICollection<Activity> activities)
        {
            int currentWeek = ISOWeek.GetWeekOfYear(date);
            return new TimeSpan(activities
                .Where(a => ISOWeek.GetWeekOfYear(DateTime.Parse(a.DayOfUse)) == currentWeek)
                .Sum(a => a.TimeSpent.Ticks));
        }
    }
}