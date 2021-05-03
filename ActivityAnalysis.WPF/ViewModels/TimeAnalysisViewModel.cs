using System.Collections.Generic;
using ActivityAnalysis.Domain.Models;
using ActivityAnalysis.WPF.State.Accounts;
using ActivityAnalysis.WPF.ViewModels.TimeAnalysisViewModels;

namespace ActivityAnalysis.WPF.ViewModels
{
    public class TimeAnalysisViewModel : ViewModelBase
    {
        public DayTimeActivityViewModel DayTimeActivityViewModel { get; }
        public WeekTimeActivityViewModel WeekTimeActivityViewModel { get; }
        public MonthTimeActivityViewModel MonthTimeActivityViewModel { get; }
        public CurrentWeekActivityViewModel CurrentWeekActivityViewModel { get; }
        public CurrentMonthActivityViewModel CurrentMonthActivityViewModel { get; }
        
        public TimeAnalysisViewModel(IAccountStore accountStore)
        {
            ICollection<Activity> activities = accountStore.CurrentAccount.Activities;
            DayTimeActivityViewModel = new DayTimeActivityViewModel(activities);
            WeekTimeActivityViewModel = new WeekTimeActivityViewModel(activities);
            MonthTimeActivityViewModel = new MonthTimeActivityViewModel(activities);
            CurrentWeekActivityViewModel = new CurrentWeekActivityViewModel(activities);
            CurrentMonthActivityViewModel = new CurrentMonthActivityViewModel(activities);
        }
    }
}