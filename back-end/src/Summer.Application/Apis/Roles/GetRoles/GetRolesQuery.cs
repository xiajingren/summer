using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Apis.Roles.GetRoles
{
    [Permission(nameof(GetRolesQuery), "获取角色列表", PermissionConstants.RoleGroupName)]
    public class GetRolesQuery : PaginationQuery, IRequest<PaginationResponse<RoleResponse>>
    {
    }
}