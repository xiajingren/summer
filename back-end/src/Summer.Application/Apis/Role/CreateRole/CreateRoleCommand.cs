using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Apis.Role.CreateRole
{
    [Permission(nameof(CreateRoleCommand), "创建角色", PermissionConstants.RoleGroupName)]
    public class CreateRoleCommand : IRequest<RoleResponse>
    {
        public string Name { get; set; }

        public CreateRoleCommand(string name)
        {
            Name = name;
        }
    }
}