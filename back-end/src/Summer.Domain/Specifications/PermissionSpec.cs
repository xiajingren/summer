using System.Collections.Generic;
using System.Linq;
using Ardalis.Specification;
using Summer.Domain.Entities;

namespace Summer.Domain.Specifications
{
    public sealed class PermissionSpec : Specification<Permission>
    {
        public PermissionSpec(int targetId, PermissionType permissionType)
        {
            Query.Where(x => x.TargetId == targetId && x.PermissionType == permissionType);
        }

        public PermissionSpec(IEnumerable<int> targetIds, PermissionType permissionType)
        {
            Query.Where(x => targetIds.Contains(x.TargetId) && x.PermissionType == permissionType);
        }
    }
}