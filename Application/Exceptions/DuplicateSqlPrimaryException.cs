using System;

namespace Application.Exceptions
{
    public class DuplicateSqlPrimaryException : Exception
    {
        public DuplicateSqlPrimaryException(string message) : base(message)
        {
        }
    }
}