using System;

namespace Domain.Exceptions
{
    public class NullUserException : Exception
    {
        public NullUserException(string message) : base(message)
        {
        }
    }
}