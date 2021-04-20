using System;

namespace Summer.App.Base.Entities
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