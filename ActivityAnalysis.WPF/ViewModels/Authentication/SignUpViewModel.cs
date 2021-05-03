using System;
using System.Net.Mail;
using System.Windows.Input;
using ActivityAnalysis.Domain.Services.EmailService;
using ActivityAnalysis.WPF.Commands;
using ActivityAnalysis.WPF.Commands.Authentication;
using ActivityAnalysis.WPF.State.Authenticators;
using ActivityAnalysis.WPF.State.Navigators;

namespace ActivityAnalysis.WPF.ViewModels.Authentication
{
    public class SignUpViewModel: ViewModelBase
    {
        private readonly IEmailService _emailService;
        private string _login;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private string _code;
        
        public ICommand ViewSignInCommand { get; }
        public ICommand SignUpCommand { get; }
        public MessageViewModel ErrorMessageViewModel { get; }
        public string GeneratedCode { get; private set; }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
                OnPropertyChanged(nameof(CanRegister));
            }
        }
        
        public string Email
        {
            get =>_email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(CanRegister));
                try
                {
                    new MailAddress(_email);
                    GeneratedCode = _emailService.GenerateVerificationKey().ToString();
                    _emailService.SendVerificationMessage(_email, GeneratedCode,VerificationMessageType.SignUpConfirmation);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        
        public string Password
        {
            get =>_password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        public string ConfirmPassword
        {
            get =>_confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
                OnPropertyChanged(nameof(CanRegister));
            }
        }
        
        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged(nameof(Code));
                OnPropertyChanged(nameof(CanRegister));
            }
        }
        
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }
        
        public bool CanRegister => !string.IsNullOrEmpty(Email) &&
                                   !string.IsNullOrEmpty(Login) &&
                                   !string.IsNullOrEmpty(Password) &&
                                   !string.IsNullOrEmpty(ConfirmPassword) &&
                                   !string.IsNullOrEmpty(Code);

        public SignUpViewModel(IAuthenticator authenticator, IEmailService emailService, IRenavigator homeRenavigator, IRenavigator signInRenavigator)
        {
            ErrorMessageViewModel = new MessageViewModel();
            _emailService = emailService;
            SignUpCommand = new SignUpCommand(this, authenticator, homeRenavigator);
            ViewSignInCommand = new RenavigateCommand(signInRenavigator);
        }
        
        public override void Dispose()
        {
            ErrorMessageViewModel.Dispose();

            base.Dispose();
        }
    }
}