using System;

namespace Summer.Shared.Exceptions
{
    public class FriendlyException : Exception
    {
        public FriendlyException()
        {

        }

        public FriendlyException(string message) : base(message)
        {

        }

        public FriendlyException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}