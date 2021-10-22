using System.Collections.Generic;
using MediatR;
using Summer.Application.Permissions;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Queries
{
    [Permission(nameof(GetRolesQuery), "获取角色列表", PermissionConstants.RoleGroupName)]
    public class GetRolesQuery : PaginationQuery, IRequest<PaginationResponse<RoleResponse>>
    {
    }
}