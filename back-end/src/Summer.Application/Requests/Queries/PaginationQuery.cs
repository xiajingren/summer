namespace Summer.Application.Requests.Queries
{
    public class PaginationQuery
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Skip => (PageIndex - 1) * PageSize;

        public PaginationQuery(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex < 1 ? 1 : pageIndex;
            PageSize = pageSize > 100 ? 100 : pageSize;
        }
    }
}