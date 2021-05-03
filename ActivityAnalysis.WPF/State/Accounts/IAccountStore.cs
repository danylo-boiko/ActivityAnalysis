using System;
using ActivityAnalysis.Domain.Models;

namespace ActivityAnalysis.WPF.State.Accounts
{
    public interface IAccountStore
    {
        Account CurrentAccount { get; set; }
        event Action StateChanged;
    }
}