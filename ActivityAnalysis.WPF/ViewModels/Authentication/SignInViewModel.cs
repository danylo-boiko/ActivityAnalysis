using System.Windows.Input;
using ActivityAnalysis.WPF.Commands;
using ActivityAnalysis.WPF.Commands.Authentication;
using ActivityAnalysis.WPF.State.Authenticators;
using ActivityAnalysis.WPF.State.Navigators;

namespace ActivityAnalysis.WPF.ViewModels.Authentication
{
    public class SignInViewModel : ViewModelBase
    {
        private string _loginOrEmail;
        private string _password;
        
        public ICommand SignInCommand { get; }
        public ICommand ViewSignUpCommand { get; }
        public ICommand ViewPasswordRecoveryCommand { get; }
        public MessageViewModel ErrorMessageViewModel { get; }

        public string LoginOrEmail
        {
            get =>_loginOrEmail;
            set
            {
                _loginOrEmail = value;
                OnPropertyChanged(nameof(LoginOrEmail));
                OnPropertyChanged(nameof(CanSignIn));
            }
        }
        
        public string Password
        {
            get =>_password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanSignIn));
            }
        }
        
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }
        
        public bool CanSignIn => !string.IsNullOrEmpty(LoginOrEmail) && !string.IsNullOrEmpty(Password);
        
        public SignInViewModel(IAuthenticator authenticator, IRenavigator homeRenavigator, IRenavigator passwordRecoveryRenavigator, IRenavigator signUpRenavigator)
        {
            ErrorMessageViewModel = new MessageViewModel();
            SignInCommand = new SignInCommand(this, authenticator, homeRenavigator);
            ViewPasswordRecoveryCommand = new RenavigateCommand(passwordRecoveryRenavigator);
            ViewSignUpCommand = new RenavigateCommand(signUpRenavigator);
        }
        
        public override void Dispose()
        {
            ErrorMessageViewModel.Dispose();

            base.Dispose();
        }
    }
}