using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ActivityAnalysis.Domain.Models;

namespace ActivityAnalysis.Domain.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IUserService _userService;
        private readonly EmailAssistant _emailAssistant;
        private readonly MailAddress _fromAddress;
        
        public EmailService(IUserService userService, EmailAssistant emailAssistant)
        {
            _userService = userService;
            _emailAssistant = emailAssistant;
            _fromAddress = new MailAddress(emailAssistant.Email, emailAssistant.Name);
        }
        
        public int GenerateVerificationKey() => new Random().Next(100000, 999999);

        public async Task SendVerificationMessage (string loginOrEmail, string confirmationCode, VerificationMessageType messageType)
        {
            MailMessage message = await GenerateVerificationMessage(loginOrEmail, confirmationCode, messageType);
            await SendMessage(message);
        }

        private async Task<MailMessage> GenerateVerificationMessage(string email, string verificationCode, VerificationMessageType messageType)
        {
            switch (messageType)
            {
                case VerificationMessageType.PasswordRecovery:
                    User user = await _userService.GetByLoginOrEmail(email);
                    return new MailMessage(_fromAddress, new MailAddress(user.Email))
                    {
                        IsBodyHtml = true,
                        Subject = "Password recovery",
                        Body = $"{user.Login}, <br>" +
                              "You have requested to recover your password on the Activity Analysis. <br>" +
                              $"Password change confirmation code: {verificationCode}"
                    };
                case VerificationMessageType.SignUpConfirmation:
                    return new MailMessage(_fromAddress, new MailAddress(email))
                    {
                        IsBodyHtml = true,
                        Subject = "Confirmation of registration",
                        Body = $"Confirm email the address {email} belongs to you. <br>" + 
                               $"To complete registration, enter the code: {verificationCode}"
                    };
                default:
                    throw new Exception($"Invalid {messageType}");
            }
        }
        
        private async Task SendMessage(MailMessage message)
        {
            using var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailAssistant.Email, _emailAssistant.Password)
            };
            await smtpClient.SendMailAsync(message);
        }
    }
}