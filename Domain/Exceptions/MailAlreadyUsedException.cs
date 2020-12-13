using System;

namespace Domain.Exceptions
{
    public class MailAlreadyUsedException : Exception
    {
        public MailAlreadyUsedException(string message) : base(message)
        {
        }
    }
}