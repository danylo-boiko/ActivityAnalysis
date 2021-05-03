using System;
using ActivityAnalysis.WPF.ViewModels;

namespace ActivityAnalysis.WPF.State.Navigators
{
    public class Navigator : INavigator
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get =>_currentViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
    }
}