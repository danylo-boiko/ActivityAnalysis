using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using ActivityAnalysis.Domain.Models;

namespace ActivityAnalysis.WPF.ViewModels.ProgramsAnalysisViewModels
{
    public class DayProgramActivityViewModel: ViewModelBase
    {
        public string DayActivity { get; }
        public string LastDayActivityPercent { get; }
        public Brush LastDayActivityPercentColor { get; }

        public DayProgramActivityViewModel(ICollection<Activity> activities)
        {
            int thisDayActivity = ActivityPerDay(DateTime.Now, activities);
            int lastDayActivity = ActivityPerDay(DateTime.Now.AddDays(-1), activities);
            double percent = (thisDayActivity | 1)  * 1.0 / (lastDayActivity | 1) * 100;
            double lastDayPercent = thisDayActivity == 0 || lastDayActivity == 0 ? 100 : percent;
            
            DayActivity = $"{thisDayActivity} programs";
            LastDayActivityPercent = $"{Math.Round(lastDayPercent, 1)} %";
            LastDayActivityPercentColor = thisDayActivity >= lastDayActivity ? Brushes.Green : Brushes.Red;
        }
        
        private int ActivityPerDay(DateTime date, ICollection<Activity> activities)
        {
            string currentDay = date.ToString("d");
            return activities
                .Where(a => a.DayOfUse.Equals(currentDay))
                .Count();
        }
    }
}