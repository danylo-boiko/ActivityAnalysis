using System.Windows.Input;
using ActivityAnalysis.Domain.Services.ProcessService;
using ActivityAnalysis.WPF.Commands;
using ActivityAnalysis.WPF.State.Authenticators;
using ActivityAnalysis.WPF.State.Navigators;
using ActivityAnalysis.WPF.ViewModels.Factories;

namespace ActivityAnalysis.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly IProcessService _processService;
        
        public HeaderViewModel HeaderViewModel { get; }
        public ICommand UpdateCurrentViewModelCommand { get; } 
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
        public bool IsLoggedIn => _authenticator.IsLoggedIn;

        public MainViewModel(HeaderViewModel headerViewModel, IViewModelFactory viewModelFactory, IAuthenticator authenticator, INavigator navigator, IProcessService processService)
        {
            HeaderViewModel = headerViewModel;
            _viewModelFactory = viewModelFactory;
            _navigator = navigator;
            _processService = processService;
            _authenticator = authenticator;
            
            _navigator.StateChanged += Navigator_StateChanged;
            _authenticator.StateChanged += Authenticator_StateChanged;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_navigator, _viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.SignIn);
        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            if (IsLoggedIn)
            {
                _processService.Start(_authenticator.CurrentAccount);
            }
            else
            {
                _processService.Stop();
            }
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public override void Dispose()
        {
            _navigator.StateChanged -= Navigator_StateChanged;
            _authenticator.StateChanged -= Authenticator_StateChanged;

            base.Dispose();
        }
    }
}