using System;

namespace Summer.Domain.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException() : base("发生了一些错误...")
        {
        }

        public BusinessException(string message) : base(message)
        {
        }
    }
}