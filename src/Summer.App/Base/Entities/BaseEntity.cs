using System;
using System.ComponentModel.DataAnnotations;

namespace Summer.App.Base.Entities
{
    internal class BaseEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreateTime = DateTime.Now;
        }
    }
}