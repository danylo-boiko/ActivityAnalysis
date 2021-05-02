using System;

namespace ActivityAnalysis.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string LoginOrEmail { get; }

        public UserNotFoundException(string message): base(message) { }

        public UserNotFoundException(string message, string loginOrEmail) : base(message)
        {
            LoginOrEmail = loginOrEmail;
        }

        public UserNotFoundException(string message, string loginOrEmail, Exception innerException) : base(message, innerException)
        {
            LoginOrEmail = loginOrEmail;
        }
    }
}