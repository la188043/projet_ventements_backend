using System;

namespace Application.Exceptions
{
    public class CouldNotUpdateAddressException : Exception
    {
        public CouldNotUpdateAddressException() : base("Une erreur s'est produite lors de la mise à jour de l'adresse")
        {
        }
    }
}