using System;
using System.Windows.Input;
using ActivityAnalysis.Domain.Services.EmailService;
using ActivityAnalysis.WPF.Commands;
using ActivityAnalysis.WPF.Commands.Authentication;
using ActivityAnalysis.WPF.State.Authenticators;
using ActivityAnalysis.WPF.State.Navigators;

namespace ActivityAnalysis.WPF.ViewModels.Authentication
{
    public class PasswordRecoveryViewModel : ViewModelBase
    {
        private readonly IEmailService _emailService;
        private string _loginOrEmail;
        private string _code;
        private string _password;
        private string _confirmPassword;
        
        public MessageViewModel ErrorMessageViewModel { get; }
        public ICommand PasswordRecoveryCommand { get; }
        public ICommand ViewSignInCommand { get; }
        public string GeneratedCode { get; private set; }

        public string LoginOrEmail
        {
            get => _loginOrEmail;
            set
            {
                _loginOrEmail = value;
                OnPropertyChanged(nameof(LoginOrEmail));
                OnPropertyChanged(nameof(CanRecovery));
                try
                {
                    GeneratedCode = _emailService.GenerateVerificationKey().ToString();
                    _emailService.SendVerificationMessage(_loginOrEmail, GeneratedCode,
                        VerificationMessageType.PasswordRecovery);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged(nameof(Code));
                OnPropertyChanged(nameof(CanRecovery));
            }
        }

        public string Password
        {
            get =>_password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanRecovery));
            }
        }

        public string ConfirmPassword
        {
            get=> _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
                OnPropertyChanged(nameof(CanRecovery));
            }
        }
        
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public bool CanRecovery => !string.IsNullOrEmpty(LoginOrEmail) &&
                                   !string.IsNullOrEmpty(Code) &&
                                   !string.IsNullOrEmpty(Password) &&
                                   !string.IsNullOrEmpty(ConfirmPassword);

        public PasswordRecoveryViewModel(IAuthenticator authenticator, IEmailService emailService, IRenavigator homeRenavigator, IRenavigator signInRenavigator)
        {
            _emailService = emailService;
            ErrorMessageViewModel = new MessageViewModel();
            PasswordRecoveryCommand = new PasswordRecoveryCommand(this, authenticator, homeRenavigator);
            ViewSignInCommand = new RenavigateCommand(signInRenavigator);
        }
        
        public override void Dispose()
        {
            ErrorMessageViewModel.Dispose();

            base.Dispose();
        }
    }
}