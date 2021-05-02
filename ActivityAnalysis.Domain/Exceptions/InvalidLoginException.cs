using System;

namespace ActivityAnalysis.Domain.Exceptions
{
    public class InvalidLoginException : Exception
    {
        public string Login { get; }

        public InvalidLoginException(string message): base(message) { }

        public InvalidLoginException(string message, string login) : base(message)
        {
            Login = login;
        }

        public InvalidLoginException(string message, string login, Exception innerException) : base(message, innerException)
        {
            Login = login;
        }
    }
}