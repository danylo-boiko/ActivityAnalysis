using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using ActivityAnalysis.Domain.Models;

namespace ActivityAnalysis.WPF.ViewModels.ProgramsAnalysisViewModels
{
    public class WeekProgramActivityViewModel: ViewModelBase
    {
        public string WeekActivity { get; }
        public string LastWeekActivityPercent { get; }
        public Brush LastWeekActivityPercentColor { get; }

        public WeekProgramActivityViewModel(ICollection<Activity> activities)
        {
            int thisWeekTime = ActivityPerWeek(DateTime.Now, activities);
            int lastWeekTime = ActivityPerWeek(DateTime.Now.AddDays(-7), activities);
            double percent = (thisWeekTime | 1) * 1.0 / (lastWeekTime | 1) * 100;
            double lastWeekPercent = thisWeekTime == 0 || lastWeekTime == 0 ? 100 : percent;

            WeekActivity = $"{thisWeekTime} programs";
            LastWeekActivityPercent = $"{Math.Round(lastWeekPercent, 1)} %";
            LastWeekActivityPercentColor = thisWeekTime >= lastWeekTime ? Brushes.Green : Brushes.Red;
        }
        
        private int ActivityPerWeek( DateTime date, ICollection<Activity> activities)
        {
            int currentWeek = ISOWeek.GetWeekOfYear(date);
            return activities
                .Where(a => ISOWeek.GetWeekOfYear(DateTime.Parse(a.DayOfUse)) == currentWeek)
                .Count();
        }
    }
}