using System;
using System.Threading.Tasks;
using ActivityAnalysis.Domain.Models;
using ActivityAnalysis.Domain.Services.AuthenticationServices;
using ActivityAnalysis.WPF.State.Accounts;

namespace ActivityAnalysis.WPF.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountStore _accountStore;

        public Authenticator(IAuthenticationService authenticationService, IAccountStore accountStore)
        {
            _authenticationService = authenticationService;
            _accountStore = accountStore;
        }

        public Account CurrentAccount
        {
            get => _accountStore.CurrentAccount;
            private set
            {
                _accountStore.CurrentAccount = value;
                StateChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentAccount != null;

        public event Action StateChanged;

        public async Task Login(string username, string password)
        {
            CurrentAccount = await _authenticationService.Login(username, password);
        }

        public async Task Register(string email, string login, string password, string confirmPassword, string code, string sentCode)
        {
            CurrentAccount = await _authenticationService.Register(email, login, password, confirmPassword,code,sentCode);
        }

        public async Task PasswordRecovery(string loginOrEmail, string password, string confirmPassword, string code, string sentCode)
        {
            CurrentAccount = await _authenticationService.PasswordRecovery(loginOrEmail, password, confirmPassword, code, sentCode);
        }
        
        public void Logout()
        {
            CurrentAccount = null;
        }
    }
}