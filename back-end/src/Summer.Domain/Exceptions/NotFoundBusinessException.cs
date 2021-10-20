namespace Summer.Domain.Exceptions
{
    public class NotFoundBusinessException : BusinessException
    {
        public NotFoundBusinessException() : base("资源未找到...")
        {
        }

        public NotFoundBusinessException(string message) : base(message)
        {
        }
    }
}