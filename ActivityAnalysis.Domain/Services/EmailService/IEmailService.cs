using System.Threading.Tasks;

namespace ActivityAnalysis.Domain.Services.EmailService
{
    public interface IEmailService
    {
        public Task SendVerificationMessage(string loginOrEmail, string verificationCode, VerificationMessageType messageType);
        public int GenerateVerificationKey();
    }
}