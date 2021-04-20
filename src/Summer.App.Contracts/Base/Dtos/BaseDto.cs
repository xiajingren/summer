namespace Summer.App.Contracts.Base.Dtos
{
    public class BaseDto<T>
    {
        public static BaseDto<T> CreateOkInstance(T data, string message = "操作成功")
        {
            return new BaseDto<T>(1, message, data);
        }

        public static BaseDto<T> CreateFailInstance(T data, string message = "操作失败")
        {
            return new BaseDto<T>(0, message, data);
        }

        protected BaseDto(int code, string message, T data)
        {
            Code = code;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// 1 成功 0 失败
        /// </summary>
        public int Code { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
