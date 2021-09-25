using System;
using Summer.Shared.Utils;

namespace Summer.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException()
        {

        }

        public DomainException(string message) : base(message)
        {

        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}