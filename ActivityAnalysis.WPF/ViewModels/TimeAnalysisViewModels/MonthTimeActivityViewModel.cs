using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using ActivityAnalysis.Domain.Models;
using ActivityAnalysis.WPF.Helpers;

namespace ActivityAnalysis.WPF.ViewModels.TimeAnalysisViewModels
{
    public class MonthTimeActivityViewModel: ViewModelBase
    {
        public string MonthActivity { get; }
        public string LastMonthActivityPercent { get; }
        public Brush LastMonthActivityPercentColor { get; }

        public MonthTimeActivityViewModel(ICollection<Activity> activities)
        {
            TimeSpan thisMonthTime = ActivityPerMonth(DateTime.Now, activities);
            TimeSpan lastMonthTime = ActivityPerMonth(DateTime.Now.AddMonths(-1), activities);
            double percent = thisMonthTime / lastMonthTime * 100;
            double lastMonthPercent = thisMonthTime == TimeSpan.Zero || lastMonthTime == TimeSpan.Zero ? 100 : percent;

            MonthActivity = thisMonthTime.SecondsToFullString();
            LastMonthActivityPercent = $"{Math.Round(lastMonthPercent, 1)} %";
            LastMonthActivityPercentColor = thisMonthTime >= lastMonthTime ? Brushes.Green : Brushes.Red;
        }
        
        private static TimeSpan ActivityPerMonth(DateTime date, ICollection<Activity> activities)
        {
            int currentMonth = date.Month;
            return new TimeSpan(activities
                .Where(a => DateTime.Parse(a.DayOfUse).Month == currentMonth)
                .Sum(a => a.TimeSpent.Ticks));
        }
    }
}