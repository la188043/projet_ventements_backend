using System;

namespace Domain.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException() : base("Mot de passe incorrect")
        {
        }
    }
}