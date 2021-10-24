using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Queries
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