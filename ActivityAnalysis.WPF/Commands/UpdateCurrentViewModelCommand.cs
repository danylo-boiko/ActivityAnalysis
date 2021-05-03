using System;
using System.Windows.Input;
using ActivityAnalysis.WPF.State.Navigators;
using ActivityAnalysis.WPF.ViewModels.Factories;

namespace ActivityAnalysis.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        private readonly INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;
        public event EventHandler CanExecuteChanged;

        public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            if(parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}