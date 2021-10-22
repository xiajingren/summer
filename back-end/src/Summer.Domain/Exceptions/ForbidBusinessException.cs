namespace Summer.Domain.Exceptions
{
    public class ForbidBusinessException : BusinessException
    {
        public ForbidBusinessException() : base("禁止访问...")
        {
        }

        public ForbidBusinessException(string message) : base(message)
        {
        }
    }
}