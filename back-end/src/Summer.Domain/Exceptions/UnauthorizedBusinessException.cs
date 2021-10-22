namespace Summer.Domain.Exceptions
{
    public class UnauthorizedBusinessException : BusinessException
    {
        public UnauthorizedBusinessException() : base("未授权...")
        {
        }

        public UnauthorizedBusinessException(string message) : base(message)
        {
        }
    }
}