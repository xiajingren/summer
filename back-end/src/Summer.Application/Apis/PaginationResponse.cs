using System.Collections.Generic;

namespace Summer.Application.Apis
{
    public class PaginationResponse<T>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int RowCount { get; set; }

        public int PageCount => (RowCount % PageSize) == 0 ? RowCount / PageSize : (RowCount / PageSize) + 1;

        public IEnumerable<T> Rows { get; set; }

        public PaginationResponse(int pageIndex, int pageSize, int rowCount, IEnumerable<T> rows)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            RowCount = rowCount;

            Rows = rows;
        }
    }
}