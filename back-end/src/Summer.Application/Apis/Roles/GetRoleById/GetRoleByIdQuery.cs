using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Apis.Roles.GetRoleById
{
    [Permission(nameof(GetRoleByIdQuery), "根据Id获取角色", PermissionConstants.RoleGroupName)]
    public class GetRoleByIdQuery : IRequest<RoleResponse>
    {
        public int Id { get; set; }

        public GetRoleByIdQuery(int id)
        {
            Id = id;
        }
    }
}