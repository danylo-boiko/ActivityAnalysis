namespace ActivityAnalysis.WPF.ViewModels.ProgramsAnalysisViewModels
{
    public class DaysOfUseRatingItemViewModel : ViewModelBase
    {
        private string _title;
        private int _daysOfUse;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        
        public int DaysOfUse
        {
            get => _daysOfUse;
            set
            {
                _daysOfUse = value;
                OnPropertyChanged(nameof(DaysOfUse));
            }
        }
    }
}