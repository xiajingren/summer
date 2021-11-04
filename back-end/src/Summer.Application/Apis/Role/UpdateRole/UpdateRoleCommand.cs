using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Apis.Role.UpdateRole
{
    [Permission(nameof(UpdateRoleCommand), "修改角色", PermissionConstants.RoleGroupName)]
    public class UpdateRoleCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UpdateRoleCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}