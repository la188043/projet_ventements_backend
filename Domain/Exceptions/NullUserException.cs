using System;

namespace Domain.Exceptions
{
    public class NullUserException : Exception
    {
        public NullUserException() : base("Adresse mail inexistante")
        {
        }
    }
}