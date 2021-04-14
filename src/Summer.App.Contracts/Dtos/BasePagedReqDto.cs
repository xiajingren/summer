namespace Summer.App.Contracts.Dtos
{
    public class BasePagedReqDto
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string Query { get; set; }

        public BasePagedReqDto()
        {
            PageIndex = 1;
            PageSize = 10;
        }
    }
}
