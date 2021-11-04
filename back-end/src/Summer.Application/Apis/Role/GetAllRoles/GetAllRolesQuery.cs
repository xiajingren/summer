using System.Collections.Generic;
using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Apis.Role.GetAllRoles
{
    [Permission(nameof(GetAllRolesQuery), "获取所有角色", PermissionConstants.RoleGroupName)]
    public class GetAllRolesQuery : IRequest<IEnumerable<RoleResponse>>
    {
    }
}