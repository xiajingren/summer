using System.Collections.Generic;
using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Apis.Permission.GetPermissionGroups
{
    [Permission(nameof(GetPermissionGroupsQuery), "获取所有权限", PermissionConstants.PermissionGroupName)]
    public class GetPermissionGroupsQuery : IRequest<IEnumerable<PermissionGroupInfoResponse>>
    {
    }
}