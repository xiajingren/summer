using System.Collections.Generic;

namespace Summer.App.Contracts.Dtos
{
    public class BasePagedDto<T>
    {
        public int Total { get; set; }

        public IEnumerable<T> List { get; set; }
    }
}
