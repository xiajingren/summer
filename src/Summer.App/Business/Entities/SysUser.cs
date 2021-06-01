using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Summer.App.Base.Entities;

namespace Summer.App.Business.Entities
{
    /// <summary>
    /// 用户
    /// </summary>
    internal class SysUser : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string Account { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        public bool IsStatic { get; set; }

        public ICollection<SysRole> SysRoles { get; set; }

        public Guid? AvatarId { get; set; }
        public UploadFile Avatar { get; set; }
    }
}