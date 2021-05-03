using System.Collections.Generic;
using System.Collections.ObjectModel;
using ActivityAnalysis.Domain.Models;
using ActivityAnalysis.WPF.State.Accounts;
using ActivityAnalysis.WPF.ViewModels.ProgramsAnalysisViewModels;

namespace ActivityAnalysis.WPF.ViewModels
{
    public class ProgramsAnalysisViewModel : ViewModelBase
    {
        private readonly TimeOfUseRatingViewModel _timeOfUseRatingViewModel;
        private readonly DaysOfUseRatingViewModel _daysOfUseRatingViewModel;
        
        public DayProgramActivityViewModel DayProgramActivityViewModel { get; }
        public WeekProgramActivityViewModel WeekProgramActivityViewModel { get; }
        public MonthProgramActivityViewModel MonthProgramActivityViewModel { get; }
        public ObservableCollection<TimeOfUseRatingItemViewModel> TimeOfUsePrograms => _timeOfUseRatingViewModel.Programs;
        public ObservableCollection<DaysOfUseRatingItemViewModel> DaysOfUsePrograms => _daysOfUseRatingViewModel.Programs;

        public ProgramsAnalysisViewModel(IAccountStore accountStore)
        {
            ICollection<Activity> activities = accountStore.CurrentAccount.Activities;
            _timeOfUseRatingViewModel = new TimeOfUseRatingViewModel(activities);
            _daysOfUseRatingViewModel = new DaysOfUseRatingViewModel(activities);
            DayProgramActivityViewModel = new DayProgramActivityViewModel(activities);
            WeekProgramActivityViewModel = new WeekProgramActivityViewModel(activities);
            MonthProgramActivityViewModel = new MonthProgramActivityViewModel(activities);
        }
    }
}