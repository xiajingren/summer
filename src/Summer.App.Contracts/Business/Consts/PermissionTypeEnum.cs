using System.ComponentModel.DataAnnotations;

namespace Summer.App.Contracts.Business.Consts
{
    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PermissionTypeEnum
    {
        /// <summary>
        /// 角色权限
        /// </summary>
        [Display(Name = "角色权限")]
        R,
        /// <summary>
        /// 用户权限
        /// </summary>
        [Display(Name = "用户权限")]
        U,
    }
}