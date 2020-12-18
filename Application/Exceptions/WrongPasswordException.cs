using System;

namespace Application.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException() : base("Mot de passe incorrect")
        {
        }
    }
}