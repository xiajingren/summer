using System.Collections.Generic;

namespace Summer.Domain.Exceptions
{
    public class DetailErrorsBusinessException : BusinessException
    {
        public IEnumerable<DetailError> DetailErrors { get; }

        public DetailErrorsBusinessException(IEnumerable<DetailError> detailErrors) : base(
            "发生了一些错误，详情请见：DetailErrors")
        {
            DetailErrors = detailErrors;
        }

        public DetailErrorsBusinessException(string message, IEnumerable<DetailError> detailErrors) : base(message)
        {
            DetailErrors = detailErrors;
        }
    }

    public class DetailError
    {
        public string Code { get; }

        public string Detail { get; }

        public DetailError(string code, string detail)
        {
            Code = code;
            Detail = detail;
        }
    }
}