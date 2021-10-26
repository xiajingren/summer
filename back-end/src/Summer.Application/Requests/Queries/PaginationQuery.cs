namespace Summer.Application.Requests.Queries
{
    public class PaginationQuery
    {
        private int _pageIndex;
        private int _pageSize;

        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value < 1 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 100 ? 100 : value;
        }

        public string Filter { get; set; }

        public int GetSkip() => (PageIndex - 1) * PageSize;
    }
}