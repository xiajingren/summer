namespace Summer.App.Contracts.Base.Dtos
{
    public class OutputDto<T>
    {
        public static OutputDto<T> CreateOkInstance(T data, string message = "操作成功")
        {
            return new OutputDto<T>(1, message, data);
        }

        public static OutputDto<T> CreateFailInstance(T data, string message = "操作失败")
        {
            return new OutputDto<T>(0, message, data);
        }

        protected OutputDto(int code, string message, T data)
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
