using System.Collections.Generic;

namespace Summer.App.Contracts.Base.Dtos
{
    public class PagedOutputDto<T>
    {
        public int Total { get; set; }

        public List<T> List { get; set; }
    }
}
