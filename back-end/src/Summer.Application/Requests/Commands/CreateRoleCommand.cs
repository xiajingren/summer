using MediatR;
using Summer.Application.Permissions;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Commands
{
    [Permission(nameof(CreateRoleCommand), "创建角色", "角色管理")]
    public class CreateRoleCommand : IRequest<RoleResponse>
    {
        public string Name { get; set; }

        public CreateRoleCommand(string name)
        {
            Name = name;
        }
    }
}