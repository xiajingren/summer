using System.Collections.Generic;
using MediatR;
using Summer.Domain.Entities;

namespace Summer.Application.Apis.Permissions.GetPermissions
{
    public class GetPermissionsQuery : IRequest<IEnumerable<PermissionResponse>>
    {
        public int TargetId { get; set; }

        public PermissionType PermissionType { get; set; }

        public GetPermissionsQuery()
        {
        }

        public GetPermissionsQuery(int targetId, PermissionType permissionType)
        {
            TargetId = targetId;
            PermissionType = permissionType;
        }
    }
}