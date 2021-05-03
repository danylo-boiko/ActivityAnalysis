namespace ActivityAnalysis.WPF.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        private string _message;
        public string Message
        {
            get =>_message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(HasMessage));
            }
        }

        public bool HasMessage => !string.IsNullOrEmpty(Message);
    }
}