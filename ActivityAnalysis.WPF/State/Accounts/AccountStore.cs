using System;
using ActivityAnalysis.Domain.Models;

namespace ActivityAnalysis.WPF.State.Accounts
{
    public class AccountStore : IAccountStore
    {
        public event Action StateChanged;
        private Account _currentAccount;

        public Account CurrentAccount
        {
            get => _currentAccount;
            set
            {
                _currentAccount = value;
                StateChanged?.Invoke();
            }
        } 
    }
}