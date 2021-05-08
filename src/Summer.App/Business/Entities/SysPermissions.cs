using System;
using System.ComponentModel.DataAnnotations;
using Summer.App.Base.Entities;
using Summer.App.Contracts.Business.Consts;

namespace Summer.App.Business.Entities
{
    /// <summary>
    /// 权限
    /// </summary>
    internal class SysPermissions : BaseEntity
    {
        /// <summary>
        /// 权限Key
        /// </summary>
        [Required]
        [StringLength(50)]
        public string PermissionKey { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        [Required]
        public PermissionTypeEnum PermissionType { get; set; }

        /// <summary>
        /// 权限对象的Id（SysRole/SysUser）
        /// </summary>
        [Required]
        public Guid PermissionEntityId { get; set; }
    }
}