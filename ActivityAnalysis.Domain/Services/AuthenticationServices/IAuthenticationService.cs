using System.Threading.Tasks;
using ActivityAnalysis.Domain.Models;

namespace ActivityAnalysis.Domain.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<Account> Login(string loginOrEmail, string password);
        Task<Account> Register(string email, string login, string password, string confirmPassword, string code, string generatedCode);
        Task<Account> PasswordRecovery(string loginOrEmail, string password, string confirmPassword, string code, string generatedCode);
    }
}