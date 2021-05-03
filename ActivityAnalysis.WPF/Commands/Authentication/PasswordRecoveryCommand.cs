using System;
using System.ComponentModel;
using System.Threading.Tasks;
using ActivityAnalysis.Domain.Exceptions;
using ActivityAnalysis.WPF.State.Authenticators;
using ActivityAnalysis.WPF.State.Navigators;
using ActivityAnalysis.WPF.ViewModels.Authentication;

namespace ActivityAnalysis.WPF.Commands.Authentication
{
    public class PasswordRecoveryCommand : AsyncCommandBase
    {
        private readonly PasswordRecoveryViewModel _passwordRecoveryViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public PasswordRecoveryCommand(PasswordRecoveryViewModel passwordRecoveryViewModel, IAuthenticator authenticator, IRenavigator homeRenavigator)
        {
            _passwordRecoveryViewModel = passwordRecoveryViewModel;
            _authenticator = authenticator;
            _renavigator = homeRenavigator;

            _passwordRecoveryViewModel.PropertyChanged += PasswordRecoveryViewModel_PropertyChanged;
        }
        
        protected override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _authenticator.PasswordRecovery(_passwordRecoveryViewModel.LoginOrEmail,_passwordRecoveryViewModel.Password,_passwordRecoveryViewModel.ConfirmPassword,
                    _passwordRecoveryViewModel.Code,_passwordRecoveryViewModel.GeneratedCode);
               
                _renavigator.Renavigate();
            }
            catch (UserNotFoundException)
            {
                _passwordRecoveryViewModel.ErrorMessage = "Login or email you entered isn’t connected to an account.";
            }
            catch(InvalidPasswordException)
            {
                _passwordRecoveryViewModel.ErrorMessage = "Passwords do not match.";
            }
            catch(InvalidVerificationCodeException)
            {
                _passwordRecoveryViewModel.ErrorMessage = "The entered code does not match the code sent to the mail.";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override bool CanExecute(object parameter) => _passwordRecoveryViewModel.CanRecovery && base.CanExecute(parameter);

        private void PasswordRecoveryViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PasswordRecoveryViewModel.CanRecovery))
            {
                OnCanExecuteChanged();
            }
        }
    }
}