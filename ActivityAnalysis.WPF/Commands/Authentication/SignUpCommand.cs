using System;
using System.ComponentModel;
using System.Threading.Tasks;
using ActivityAnalysis.Domain.Exceptions;
using ActivityAnalysis.WPF.State.Authenticators;
using ActivityAnalysis.WPF.State.Navigators;
using ActivityAnalysis.WPF.ViewModels.Authentication;

namespace ActivityAnalysis.WPF.Commands.Authentication
{
    public class SignUpCommand : AsyncCommandBase
    {
        private readonly SignUpViewModel _signUpViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public SignUpCommand(SignUpViewModel signUpViewModel, IAuthenticator authenticator, IRenavigator homeRenavigator)
        {
            _signUpViewModel = signUpViewModel;
            _authenticator = authenticator;
            _renavigator = homeRenavigator;

            _signUpViewModel.PropertyChanged += SignUpViewModel_PropertyChanged;
        }
        
        protected override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _authenticator.Register(_signUpViewModel.Email, _signUpViewModel.Login,
                    _signUpViewModel.Password,_signUpViewModel.ConfirmPassword, _signUpViewModel.Code, _signUpViewModel.GeneratedCode);

                _renavigator.Renavigate();
            }
            catch (InvalidLoginException)
            {
                _signUpViewModel.ErrorMessage = "An account with this login is already registered.";
            }
            catch(InvalidEmailException)
            {
                _signUpViewModel.ErrorMessage = "An account with this mail is already registered.";
            }
            catch(InvalidPasswordException)
            {
                _signUpViewModel.ErrorMessage = "Passwords do not match.";
            }
            catch(InvalidVerificationCodeException)
            {
                _signUpViewModel.ErrorMessage = "The entered code does not match the code sent to the mail.";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override bool CanExecute(object parameter) =>_signUpViewModel.CanRegister && base.CanExecute(parameter);

        private void SignUpViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SignUpViewModel.CanRegister))
            {
                OnCanExecuteChanged();
            }
        }
    }
}