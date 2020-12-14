using System;

namespace Domain.Exceptions
{
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException() : base("Cette objet est déjà présent dans la liste")
        {
        }
    }
}