using System.Linq;
using Ardalis.Specification;
using Summer.Domain.Entities;

namespace Summer.Domain.Specifications
{
    public sealed class RolePermissionSpec : Specification<RolePermission, string>
    {
        public RolePermissionSpec(int[] roleIds)
        {
            Query.Where(x => roleIds.Contains(x.RoleId));
            Query.Select(x => x.PermissionCode);
        }

        public RolePermissionSpec(int roleId)
        {
            Query.Where(x => x.RoleId == roleId);
            Query.Select(x => x.PermissionCode);
        }
    }
}