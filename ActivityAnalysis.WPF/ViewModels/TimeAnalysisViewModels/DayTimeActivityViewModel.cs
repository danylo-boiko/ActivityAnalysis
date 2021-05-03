using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using ActivityAnalysis.Domain.Models;
using ActivityAnalysis.WPF.Helpers;

namespace ActivityAnalysis.WPF.ViewModels.TimeAnalysisViewModels
{
    public class DayTimeActivityViewModel : ViewModelBase
    {
        public string DayActivity { get; }
        public string LastDayActivityPercent { get; }
        public Brush LastDayActivityPercentColor { get; }

        public DayTimeActivityViewModel(ICollection<Activity> activities)
        {
            TimeSpan thisDayActivity = ActivityPerDay(DateTime.Now, activities);
            TimeSpan lastDayActivity = ActivityPerDay(DateTime.Now.AddDays(-1), activities);
            double percent = thisDayActivity / lastDayActivity * 100;
            double lastDayPercent = thisDayActivity == TimeSpan.Zero || lastDayActivity == TimeSpan.Zero ? 100 : percent;
            
            DayActivity = thisDayActivity.SecondsToFullString();
            LastDayActivityPercent = $"{Math.Round(lastDayPercent, 1)} %";
            LastDayActivityPercentColor = thisDayActivity >= lastDayActivity ? Brushes.Green : Brushes.Red;
        }
        
        private TimeSpan ActivityPerDay(DateTime date, ICollection<Activity> activities)
        {
            string currentDay = date.ToString("d");
            return new TimeSpan(activities
                .Where(a => a.DayOfUse.Equals(currentDay))
                .Sum(a => a.TimeSpent.Ticks));
        }
    }
}