using System;
using System.Collections.Generic;
using System.Text;

namespace Summer.App.Entities
{
    internal class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreateTime = DateTime.Now;
        }
    }
}