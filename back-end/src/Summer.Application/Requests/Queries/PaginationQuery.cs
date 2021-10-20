using Microsoft.AspNetCore.Mvc;

namespace Summer.Application.Requests.Queries
{
    public class PaginationQuery
    {
        [FromQuery(Name = "pageIndex")] public int PageIndex { get; set; }

        [FromQuery(Name = "pageSize")] public int PageSize { get; set; }

        public int GetSkip() => (PageIndex - 1) * PageSize;

        public PaginationQuery(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex < 1 ? 1 : pageIndex;
            PageSize = pageSize > 100 ? 100 : pageSize;
        }
    }
}