using System;
using System.Collections.Generic;
using System.Linq;

namespace Summer.Shared.Dtos
{
    public class OutputDto<T> where T : class
    {

        public OutputDto(T data)
        {
            Data = data;
        }

        public OutputDto(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public bool Success => Errors == null || !Errors.Any();

        public IEnumerable<string> Errors { get; set; }

        public T Data { get; set; }
    }

}