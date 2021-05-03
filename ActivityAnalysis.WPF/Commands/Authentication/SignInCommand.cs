using System;
using System.ComponentModel;
using System.Threading.Tasks;
using ActivityAnalysis.Domain.Exceptions;
using ActivityAnalysis.WPF.State.Authenticators;
using ActivityAnalysis.WPF.State.Navigators;
using ActivityAnalysis.WPF.ViewModels.Authentication;

namespace ActivityAnalysis.WPF.Commands.Authentication
{
    public class SignInCommand : AsyncCommandBase
    {
        private readonly SignInViewModel _signInViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public SignInCommand(SignInViewModel signInViewModel, IAuthenticator authenticator, IRenavigator homeRenavigator)
        {
            _signInViewModel = signInViewModel;
            _authenticator = authenticator;
            _renavigator = homeRenavigator;

            _signInViewModel.PropertyChanged += SignInViewModel_PropertyChanged;
        }
        
        protected override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _authenticator.Login(_signInViewModel.LoginOrEmail, _signInViewModel.Password);
                _renavigator.Renavigate();
            }
            catch (UserNotFoundException)
            {
                _signInViewModel.ErrorMessage = "Login or email you entered isn’t connected to an account.";
            }
            catch(InvalidPasswordException)
            {
                _signInViewModel.ErrorMessage = "Wrong password. Try again or recover.";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override bool CanExecute(object parameter) =>_signInViewModel.CanSignIn && base.CanExecute(parameter);

        private void SignInViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SignInViewModel.CanSignIn))
            {
                OnCanExecuteChanged();
            }
        }
    }
}