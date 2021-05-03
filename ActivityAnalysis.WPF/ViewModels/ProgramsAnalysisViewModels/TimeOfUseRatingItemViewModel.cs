using System;

namespace ActivityAnalysis.WPF.ViewModels.ProgramsAnalysisViewModels
{
    public class TimeOfUseRatingItemViewModel : ViewModelBase
    {
        private string _programTitle;
        private TimeSpan _timeSpent;
        private double _percentageOfTotal;

        public string ProgramTitle
        {
            get => _programTitle;
            set
            {
                _programTitle = value;
                OnPropertyChanged(nameof(ProgramTitle));
            }
        }
        
        public TimeSpan TimeSpent
        {
            get => _timeSpent;
            set
            {
                _timeSpent = value;
                OnPropertyChanged(nameof(TimeSpent));
            }
        }
        
        public double PercentageOfTotal
        {
            get => _percentageOfTotal;
            set
            {
                _percentageOfTotal = value;
                OnPropertyChanged(nameof(PercentageOfTotal));
            }
        }
    }
}