using System.Collections.Generic;

namespace Summer.Domain.Exceptions
{
    public class ErrorsBusinessException : BusinessException
    {
        public IEnumerable<string> Errors { get; }

        public ErrorsBusinessException(IEnumerable<string> errors) : base("发生了一些错误，详情请见：Errors")
        {
            Errors = errors;
        }

        public ErrorsBusinessException(string message, IEnumerable<string> errors) : base(message)
        {
            Errors = errors;
        }
    }
}