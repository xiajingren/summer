using System.Collections.Generic;
using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Queries
{
    [Permission(nameof(GetAllRolesQuery), "获取所有角色", PermissionConstants.RoleGroupName)]
    public class GetAllRolesQuery : IRequest<IEnumerable<RoleResponse>>
    {
    }
}