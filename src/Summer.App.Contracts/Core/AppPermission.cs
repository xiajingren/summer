using System;
using System.Collections.Generic;

namespace Summer.App.Contracts.Core
{
    /// <summary>
    /// 权限
    /// </summary>
    public class AppPermission
    {
        public ICollection<Permission> Permissions { get; set; }

        public AppPermission()
        {
            Permissions = new List<Permission>();

            var sysUserPermission = new Permission(PermissionConst.SysUser, "用户管理")
                .AddChild(PermissionConst.SysUserDefault, "默认")
                .AddChild(PermissionConst.SysUserCreate, "创建")
                .AddChild(PermissionConst.SysUserUpdate, "修改")
                .AddChild(PermissionConst.SysUserDelete, "删除");

            Permissions.Add(sysUserPermission);
        }
    }

    public class Permission
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public ICollection<Permission> Child { get; set; }

        public Permission AddChild(string key, string name)
        {
            Child.Add(new Permission(key, name));
            return this;
        }

        public Permission(string key, string name)
        {
            Key = key;
            Name = name;

            Child = new List<Permission>();
        }
    }

    public static class PermissionConst
    {
        public static string SysUser = "SysUser";
        public static string SysUserDefault = SysUser + ".Default";
        public static string SysUserCreate = SysUser + ".Create";
        public static string SysUserUpdate = SysUser + ".Update";
        public static string SysUserDelete = SysUser + ".Delete";
    }

}