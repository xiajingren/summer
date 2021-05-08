using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Summer.App.Base.Entities;

namespace Summer.App.Business.Entities
{
    /// <summary>
    /// 角色
    /// </summary>
    internal class SysRole : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        public bool IsStatic { get; set; }

        public ICollection<SysUser> SysUsers { get; set; }
    }
}