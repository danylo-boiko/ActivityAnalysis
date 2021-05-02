namespace ActivityAnalysis.Domain.Services.EmailService
{
    public class EmailAssistant
    {
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }

        public EmailAssistant(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}