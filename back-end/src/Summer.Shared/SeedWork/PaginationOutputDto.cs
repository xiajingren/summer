using System.Collections.Generic;

namespace Summer.Shared.SeedWork
{
    public class PaginationOutputDto<T>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int RowCount { get; set; }

        public int PageCount { get; set; }

        public IEnumerable<T> Rows { get; set; }
    }
}