using System;

namespace ActivityAnalysis.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public string Password { get; }

        public InvalidPasswordException(string message): base(message) { }

        public InvalidPasswordException(string message, string password) : base(message)
        {
            Password = password;
        }

        public InvalidPasswordException(string message, string password, Exception innerException) : base(message, innerException)
        {
            Password = password;
        }
    }
}