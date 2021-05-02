using System;

namespace ActivityAnalysis.Domain.Exceptions
{
    public class InvalidVerificationCodeException : Exception
    {
        public string VerificationCode { get; }

        public InvalidVerificationCodeException(string message) : base(message) { }

        public InvalidVerificationCodeException(string message, string verificationCode) : base(message)
        {
            VerificationCode = verificationCode;
        }

        public InvalidVerificationCodeException(string message, string verificationCode, Exception innerException) : base(message, innerException)
        {
            VerificationCode = verificationCode;
        }
    }
}