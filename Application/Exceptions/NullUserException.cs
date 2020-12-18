using System;

namespace Application.Exceptions
{
    public class NullUserException : Exception
    {
        public NullUserException() : base("Adresse mail inexistante")
        {
        }
    }
}