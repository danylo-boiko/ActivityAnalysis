using System;

namespace ActivityAnalysis.Domain.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public string Email { get; }

        public InvalidEmailException(string message): base(message) { }

        public InvalidEmailException(string message, string email) : base(message)
        {
            Email = email;
        }

        public InvalidEmailException(string message, string email, Exception innerException) : base(message, innerException)
        {
            Email = email;
        }
    }
}