using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using ActivityAnalysis.Domain.Models;

namespace ActivityAnalysis.WPF.ViewModels.ProgramsAnalysisViewModels
{
    public class MonthProgramActivityViewModel: ViewModelBase
    {
        public string MonthActivity { get; }
        public string LastMonthActivityPercent { get; }
        public Brush LastMonthActivityPercentColor { get; }

        public MonthProgramActivityViewModel(ICollection<Activity> activities)
        {
            int thisMonthTime = ActivityPerMonth(DateTime.Now, activities);
            int lastMonthTime = ActivityPerMonth(DateTime.Now.AddMonths(-1), activities);
            double percent = (thisMonthTime | 1) * 1.0 / (lastMonthTime | 1) * 100;
            double lastMonthPercent = thisMonthTime == 0 || lastMonthTime == 0 ? 100 : percent;

            MonthActivity = $"{thisMonthTime} programs";
            LastMonthActivityPercent = $"{Math.Round(lastMonthPercent, 1)} %";
            LastMonthActivityPercentColor = thisMonthTime >= lastMonthTime ? Brushes.Green : Brushes.Red;
        }
        
        private int ActivityPerMonth(DateTime date, ICollection<Activity> activities)
        {
            int currentMonth = date.Month;
            return activities
                .Where(a => DateTime.Parse(a.DayOfUse).Month == currentMonth)
                .Count();
        }
    }
}