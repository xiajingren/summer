namespace Summer.App.Contracts.Base.Dtos
{
    public class PagedInputDto
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string Query { get; set; }

        public PagedInputDto()
        {
            PageIndex = 1;
            PageSize = 10;
        }
    }
}
