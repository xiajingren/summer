using MediatR;
using Summer.Application.Permissions;

namespace Summer.Application.Requests.Commands
{
    [Permission(nameof(DeleteRoleCommand), "删除角色", PermissionConstants.RoleGroupName)]
    public class DeleteRoleCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteRoleCommand(int id)
        {
            Id = id;
        }
    }
}