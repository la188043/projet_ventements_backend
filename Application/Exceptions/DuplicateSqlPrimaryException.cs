using System;

namespace Domain.Exceptions
{
    public class DuplicateSqlPrimaryException : Exception
    {
        public DuplicateSqlPrimaryException(string message) : base(message)
        {
        }
    }
}