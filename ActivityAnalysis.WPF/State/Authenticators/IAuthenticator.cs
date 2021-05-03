using System;
using System.Threading.Tasks;
using ActivityAnalysis.Domain.Models;

namespace ActivityAnalysis.WPF.State.Authenticators
{
    public interface IAuthenticator
    {
        event Action StateChanged;
        
        Account CurrentAccount { get; }
        bool IsLoggedIn { get; }
        
        Task Login(string loginOrEmail, string password);
        Task Register(string email, string login, string password, string confirmPassword, string code, string sentCode);
        Task PasswordRecovery(string loginOrEmail, string password, string confirmPassword, string code, string sentCode);

        void Logout();
    }
}