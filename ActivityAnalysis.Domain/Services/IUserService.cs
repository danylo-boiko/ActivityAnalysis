using System.Threading.Tasks;
using ActivityAnalysis.Domain.Models;

namespace ActivityAnalysis.Domain.Services
{
    public interface IUserService : IDataService<User>
    {
        Task<User> GetByLoginOrEmail(string loginOrEmail);
        Task<bool> IsLoginUsed(string login);
        Task<bool> IsEmailUsed(string email);
    }
}