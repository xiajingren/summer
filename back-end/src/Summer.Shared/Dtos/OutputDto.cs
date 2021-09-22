using System.Collections.Generic;

namespace Summer.Shared.Dtos
{
    public class OutputDto<T> where T : class
    {
        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public T Data { get; set; }
    }
}