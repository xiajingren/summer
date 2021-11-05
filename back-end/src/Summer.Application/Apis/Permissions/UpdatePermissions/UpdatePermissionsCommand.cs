using System.Collections.Generic;
using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;
using Summer.Domain.Entities;

namespace Summer.Application.Apis.Permissions.UpdatePermissions
{
    [Permission(nameof(UpdatePermissionsCommand), "修改权限", PermissionConstants.PermissionGroupName)]
    public class UpdatePermissionsCommand : IRequest
    {
        public int TargetId { get; set; }

        public PermissionType PermissionType { get; set; }

        public IEnumerable<string> PermissionCodes { get; set; }

        public UpdatePermissionsCommand(int targetId, PermissionType permissionType, IEnumerable<string> permissionCodes)
        {
            TargetId = targetId;
            PermissionType = permissionType;
            PermissionCodes = permissionCodes ?? new List<string>();
        }
    }
}