using System.Collections.Generic;
using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Queries
{
    [Permission(nameof(GetPermissionGroupsQuery), "获取所有权限", PermissionConstants.PermissionGroupName)]
    public class GetPermissionGroupsQuery : IRequest<IEnumerable<PermissionGroupInfoResponse>>
    {
    }
}