using System.Globalization;
using System.Windows.Input;
using ActivityAnalysis.WPF.Commands.Authentication;
using ActivityAnalysis.WPF.State.Accounts;
using ActivityAnalysis.WPF.State.Authenticators;
using ActivityAnalysis.WPF.State.Navigators;

namespace ActivityAnalysis.WPF.ViewModels
{
    public class HeaderViewModel: ViewModelBase
    {
        private readonly IAccountStore _accountStore;
        
        public ICommand LogoutCommand { get; }
        public string Login
        {
            get 
            { 
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                return textInfo.ToTitleCase(_accountStore.CurrentAccount.AccountHolder.Login);
            }
        }

        public HeaderViewModel(IAccountStore accountStore, IAuthenticator authenticator, IRenavigator renavigator)
        {
            LogoutCommand = new LogoutCommand(authenticator,renavigator);
            _accountStore = accountStore;
            _accountStore.StateChanged += AccountStore_StateChanged;
        }
        
        private void AccountStore_StateChanged()
        {
            OnPropertyChanged(nameof(Login));
        }
    }
}