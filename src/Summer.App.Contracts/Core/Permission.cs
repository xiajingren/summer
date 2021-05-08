using System.Collections.Generic;

namespace Summer.App.Contracts.Core
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Permission
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public IEnumerable<Permission> Child { get; set; }

        public Permission()
        {
            Child = new List<Permission>();
        }
    }
}